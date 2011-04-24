package com.example.helloandroid;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.app.AlertDialog;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Environment;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class HelloAndroid extends Activity {
	
	private static final String TAG = "LINKService";
	private static final boolean D = false;
	TextView tv;
	//times
	private long linkRTT1 = 0;
	private long linkRTT2 = 0;
	private long linkRTT = 0;
	private long lbsRTT = 0;
	private long lcaRTT = 0;
	private long lbsResp = 0;
	private long startDisc1 = 0;
	private long startDisc2 = 0;
	private long discTime = 0;
	private long sign = 0;
	private long verify = 0;
	public static List<String> blConn = new ArrayList<String>();
	
	private static final int REQUEST_ENABLE_BT = 2;
	private BluetoothAdapter mBluetoothAdapter = null;
	private List<BluetoothDevice> mNewBluetoothDevices = null;
	private BluetoothChatService mChatService = null;
	
	DigitalSignature ds;
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        TextView titleView = new TextView(this);      
        titleView.setText("Hello, LINK");
        setContentView(R.layout.main);
        
        //get textview object
        tv = (TextView) findViewById(R.id.textView1);
        tv.setMovementMethod(new ScrollingMovementMethod());
        // intiate Link listener
        initiateListener();
        
        // Get local Bluetooth adapter
        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();        
        
        // If the adapter is null, then Bluetooth is not supported
        if (mBluetoothAdapter == null) {
            Toast.makeText(this, "Bluetooth is not available", Toast.LENGTH_LONG).show();
            finish();
            return;
        }
        
        // Register for broadcasts when a device is discovered
        IntentFilter filter = new IntentFilter(BluetoothDevice.ACTION_FOUND);
        this.registerReceiver(mReceiver, filter);
        
        // Register for broadcasts when discovery has finished
        filter = new IntentFilter(BluetoothAdapter.ACTION_DISCOVERY_FINISHED);
        this.registerReceiver(mReceiver, filter);
        
        //verifier
        //new Thread(new Verifier(getApplicationContext(), HelloAndroid.this)).start();
        
        //Initiate verifier thread
        mChatService = new BluetoothChatService(getApplicationContext(),HelloAndroid.this);

        if (mChatService != null) {
            // Only if the state is STATE_NONE, do we know that we haven't started already
            if (mChatService.getState() == BluetoothChatService.STATE_NONE) {
              // Start the Bluetooth chat services
              mChatService.start();
            }
        }
        
        //Lastly initiate Digital Signature
        ds = new DigitalSignature();
    }
    
    
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.option_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
        case R.id.discoverable:
            // Ensure this device is discoverable by others
            ensureDiscoverable();
            return true;
        }
        return false;
    }
    
    private void ensureDiscoverable() {
        if(D) Log.d(TAG, "ensure discoverable");
        if (mBluetoothAdapter.getScanMode() !=
            BluetoothAdapter.SCAN_MODE_CONNECTABLE_DISCOVERABLE) {
            Intent discoverableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_DISCOVERABLE);
            discoverableIntent.putExtra(BluetoothAdapter.EXTRA_DISCOVERABLE_DURATION, 300);
            startActivity(discoverableIntent);
        }
    }
    @Override
    public void onStart() {
        super.onStart();
        if(D) Log.e(TAG, "++ ON START ++");

        // If BT is not on, request that it be enabled.
        // setupChat() will then be called during onActivityResult
        if (!mBluetoothAdapter.isEnabled()) {
            Intent enableIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
            startActivityForResult(enableIntent, REQUEST_ENABLE_BT);
        // Otherwise, setup the chat session
        } 
    }
    
    @Override
    public void onDestroy() {
        super.onDestroy();
        
     // Make sure we're not doing discovery anymore
        if (mBluetoothAdapter != null) {
        	mBluetoothAdapter.cancelDiscovery();
        }

        // Unregister broadcast listeners
        this.unregisterReceiver(mReceiver);
        
        // Stop the Bluetooth chat services
        if (mChatService != null) mChatService.stop();
        if(D) Log.e(TAG, "--- ON DESTROY ---");
    }
    
    /**
     * Sends a message.
     * @param message  A string of text to send.
     */
    private void sendMessage(String message) {
        // Check that we're actually connected before trying anything
        if (mChatService.getState() != BluetoothChatService.STATE_CONNECTED) {
            Toast.makeText(this, R.string.not_connected, Toast.LENGTH_SHORT).show();
            return;
        }

        // Check that there's actually something to send
        if (message.length() > 0) {
            // Get the message bytes and tell the BluetoothChatService to write
            byte[] send = message.getBytes();
            mChatService.write(send);

         
        }
    }
    
    private void connect(BluetoothDevice device){
    	mChatService.connect(device);
    }
    
    public void initiateListener(){
    	//Below line is for testing signing
    	//testSignVerify();
    	Button linkButton = (Button) findViewById(R.id.button1);
        linkButton.setOnClickListener(new OnClickListener() {
            public void onClick(View v) {
            	//clear the times
            	sign = 0;
            	verify = 0;
            	blConn.clear();
            	
            	linkRTT1 = System.currentTimeMillis();
            	//sign 
            	long sign1 = System.currentTimeMillis();
            	String loc = "My Current Location";
        		byte[] signature = ds.sign(loc, ds.rsaPrivkey);
        		long sign2 = System.currentTimeMillis();
        		sign = sign + (sign2 - sign1);
            	
            	//LBS call to request coupon
            	Context context = getApplicationContext();
         	   	Claimer cl = new Claimer(context,HelloAndroid.this);
        		long lbs1 = System.currentTimeMillis();
        		cl.sendToLBS();
        		long lbs2 = System.currentTimeMillis();
        		lbsRTT = lbs2 - lbs1;
        		if(D) showToast("LBS RTT: "+lbsRTT);
        		//tv.append("LBS RTT: "+lbs+" \r\n");
        		
        		//verify
        		long verify1 = System.currentTimeMillis();
        		boolean ver = ds.verify(loc.getBytes(), ds.rsaPubkey, signature);
        		long verify2 = System.currentTimeMillis();
        		verify = verify + (verify2 - verify1);
        		
            	//Initialize new bluetooth devices list to hold them
                mNewBluetoothDevices = new ArrayList<BluetoothDevice>();
        		
                //Start discovery
            	//doDiscovery();
            	claim();
            	//v.setVisibility(View.GONE);
            	//startLink(v);
            	//linkAlert(v);
            }
        });
    }
    
    public void startLink(View v){    	
    	Context context = getApplicationContext();
 	   	Claimer cl = new Claimer(context,HelloAndroid.this);
 	   	cl.claim();
    }
    
    /**
     * Start device discover with the BluetoothAdapter
     */
    private void doDiscovery() {
    	startDisc1 = System.currentTimeMillis();
        //if (D) showToast("start discovery");

        // Indicate scanning in the title
        //setProgressBarIndeterminateVisibility(true);
    	//tv.append(""+R.string.scanning);

        // If we're already discovering, stop it
    	//if (D) showToast(mBluetoothAdapter.toString());
//        if (mBluetoothAdapter.isDiscovering()) {
//        	mBluetoothAdapter.cancelDiscovery();
//        }

        // Request discover from BluetoothAdapter
        mBluetoothAdapter.startDiscovery();
        if (D) showToast("Scan started");

        //Initiate new thread. This will stop scan after 5 secs
        Context context = getApplicationContext();
 	   	Claimer cl = new Claimer(startDisc1,mNewBluetoothDevices,context,HelloAndroid.this);
        new Thread(cl).start();
        
    }
    
 // The BroadcastReceiver that listens for discovered devices and
    // changes the title when discovery is finished
    private final BroadcastReceiver mReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();

            // When discovery finds a device
            if (BluetoothDevice.ACTION_FOUND.equals(action)) {
                // Get the BluetoothDevice object from the Intent
                BluetoothDevice device = intent.getParcelableExtra(BluetoothDevice.EXTRA_DEVICE);
                // If it's already paired, skip it, because it's been listed already
//                if (device.getBondState() != BluetoothDevice.BOND_BONDED) {
//                    mNewBluetoothDevices.add(device);
//                }
                long nameTime1 = System.currentTimeMillis();
                String name = device.getName();
                long nameTime2 = System.currentTimeMillis();
                long nameTime = nameTime2 - nameTime1;
                //if (D) showToast(name+" name lookup took: "+nameTime);
                if (!mNewBluetoothDevices.contains(device))
                	mNewBluetoothDevices.add(device);
                //tv.append("found device: "+device.getAddress()+" \r\n");
            // When discovery is finished, change the Activity title
            } else if (BluetoothAdapter.ACTION_DISCOVERY_FINISHED.equals(action)) {
            	startDisc2 = System.currentTimeMillis();
            	discTime = startDisc2 - startDisc1;
            	//tv.append("Discovery Time: "+discTime+" \r\n");
                //setProgressBarIndeterminateVisibility(false);
            	if (D) showToast("scan completed. ");
            	//tv.append("Found devices: "+" \r\n");
                if (mNewBluetoothDevices.size() == 0) {
                    //tv.append("No devices found \r\n");
                } else {
                	for (int i=0;i< mNewBluetoothDevices.size();i++) {
                		BluetoothDevice bt = mNewBluetoothDevices.get(i);
                		//tv.append(bt.getAddress()+" - "+bt.getName()+" \r\n");
                		
                		//Testing
//                        if(bt.getName().contains("DROID")){
//                        	connect(bt);
//                        	while(true){
//                        		if(mChatService.getState() == BluetoothChatService.STATE_CONNECTED){
//                        			sendMessage("2");
//                        			break;
//                        		}
//                        			
//                        	}
//                        	
//                        	tv.append(" message sent to "+bt.getName()+" \r\n");
//                        }
                        
					}
                }
                
                //start claim process 
                claim();                
            }
        }

		
    };
    
    private void claim() {

    	String id =""+mBluetoothAdapter.getName().charAt(5);
    	
    	long sign1 = System.currentTimeMillis();
		byte[] signature = ds.sign(id, ds.rsaPrivkey);
		long sign2 = System.currentTimeMillis();
		sign = sign + (sign2 - sign1);
    	
        //Send claim to LCA and verifiers
 	   	Claimer cl = new Claimer(startDisc1,mNewBluetoothDevices,getApplicationContext(),HelloAndroid.this);
		//LCA call to claim location
		long lca1 = System.currentTimeMillis();
		cl.sendToLCA(id);//this call populates the static trID variable
		long lca2 = System.currentTimeMillis();
		lcaRTT = lca2 - lca1;
		if(D) showToast("LCA RTT: "+lcaRTT);
        
		long verify1 = System.currentTimeMillis();
		boolean ver = ds.verify(id.getBytes(), ds.rsaPubkey, signature);
		long verify2 = System.currentTimeMillis();
		verify = verify + (verify2 - verify1);
		
		//bluetooth calls
        for (int i=0;i< mNewBluetoothDevices.size();i++) {
    		BluetoothDevice bt = mNewBluetoothDevices.get(i);
    		//filter
            if(bt.getName().contains("DROID")){
            	//connect
            	mChatService.stop();
            	if(mChatService.getState() != BluetoothChatService.STATE_CONNECTED)
            		connect(bt);
            	//send message to verifier with trID
            	long startConn = System.currentTimeMillis();
            	long stopConn = System.currentTimeMillis();
    			while((stopConn - startConn) < 5000){
            		if(mChatService.getState() == BluetoothChatService.STATE_CONNECTED){
            			//sign
            			long sign11 = System.currentTimeMillis();
            			byte[] signature1 = ds.sign(cl.trID, ds.rsaPrivkey);
            			long sign12 = System.currentTimeMillis();
            			sign = sign + (sign12 - sign11);
            			
            			sendMessage(cl.trID);
            			mChatService.stop();
            			mChatService.start();
            			break;
            		}
            		
            		stopConn = System.currentTimeMillis();
            			
            	}
            	
            	//tv.append(" message sent to "+bt.getName()+" \r\n");
            }
            
		}
        
        //wait for LBS coupon response
		long lbsResp1 = System.currentTimeMillis();
		cl.recieve();
		long lbsResp2 = System.currentTimeMillis();
		lbsResp = lbsResp2 - lbsResp1;
		if(D) showToast("lbsResp time: "+lbsResp);
		
		long verify11 = System.currentTimeMillis();
		boolean ver1 = ds.verify(id.getBytes(), ds.rsaPubkey, signature);
		long verify12 = System.currentTimeMillis();
		verify = verify + (verify2 - verify1);
		
		linkRTT2 = System.currentTimeMillis();
		linkRTT = linkRTT2 - linkRTT1;
		
		print();
		
	}

	private void print() {
		tv.append("LBS RTT: "+lbsRTT+" \r\n");
		tv.append("BL Disc: "+discTime+" \r\n");
		tv.append("LCA RTT: "+lcaRTT+" \r\n");
		tv.append("Sign: "+sign+" \r\n");
		tv.append("Verify: "+verify+" \r\n");
		tv.append("LBSResp time: "+lbsResp+" \r\n");
		String allBLConns ="";
		for(int i =0;i<blConn.size();i++){
			allBLConns = allBLConns + blConn.get(i)+",";
			tv.append("BL Conn"+i+": "+blConn.get(i)+" \r\n");
		}
		tv.append("LINK RTT: "+linkRTT+" \r\n");	
		String times = lbsRTT+","+discTime+","+lcaRTT+","+sign+","+verify+","+lbsResp+","+allBLConns+linkRTT+" \r\n";
		List<String> data = new ArrayList<String>();
		data.add(times);
		
		FileAccess.writeToFile(data);
	}

    public void linkAlert(View v){
    	AlertDialog.Builder builder = new AlertDialog.Builder(this);
    	builder.setMessage("Are you sure you want to request a coupon?")
    	       .setCancelable(false)
    	       .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
    	           public void onClick(DialogInterface dialog, int id) {
    	                //HelloAndroid.this.finish();
    	        	   //Call LINK Process
    	        	   Context context = getApplicationContext();
    	        	   Claimer cl = new Claimer(context,HelloAndroid.this);
    	        	   cl.claim();
    	           }
    	       })
    	       .setNegativeButton("No", new DialogInterface.OnClickListener() {
    	           public void onClick(DialogInterface dialog, int id) {
    	                dialog.cancel();
    	           }
    	       });
    	AlertDialog alert = builder.create();
    	alert.show();
    }
    
    public void showToast(String msg){
		CharSequence text = msg;
	 	   int duration = Toast.LENGTH_SHORT;

	 	   Toast toast = Toast.makeText(this, text, duration);
	 	   toast.show();
	}
    
//    public void testSignVerify(){
//    	Button signButton = (Button) findViewById(R.id.button2);
//    	signButton.setOnClickListener(new OnClickListener() {
//            public void onClick(View v) {
//            	Context context = getApplicationContext();
//	        	   Claimer cl = new Claimer(context,HelloAndroid.this);
//	        	   cl.sendToServer();
//            }
//        });
//    }
    
}