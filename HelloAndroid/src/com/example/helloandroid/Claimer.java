package com.example.helloandroid;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.List;
import java.util.Scanner;
import java.util.TimerTask;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.content.Context;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

public class Claimer implements Runnable{
	
	private static final boolean D = false;
	//private static final String IPAddress = "10.1.170.85";
	private static final String IPAddress = "manoop.dyndns.org";
	//private static final String IPAddress = "173.3.201.174";
	InetAddress serverAddr = null;
	long startDisc = 0;
	
	public String trID = ""; 
	
	public Context context;
	public Activity activity;
	TextView tv;
	List<BluetoothDevice> mNewBluetoothDevices;
	private BluetoothAdapter mBluetoothAdapter = null;
	
	public void run() {
		try {
			long stopDisc = System.currentTimeMillis();
			while((stopDisc - startDisc) < 9000){
				stopDisc = System.currentTimeMillis();
			}
			//cancel ongoing bluetooth discovery after 5sec
			mBluetoothAdapter.cancelDiscovery();
			
			
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public Claimer(Context cntxt, Activity actvty){
		context = cntxt;
		activity = actvty;
		tv = (TextView) activity.findViewById(R.id.textView1);
		try {
			serverAddr = InetAddress.getByName(IPAddress);
		} catch (UnknownHostException e) {
			
			e.printStackTrace();
			String info3 = "Error while server IP lookup!! \n";
			if(D) showToast(info3 + e.toString());
		}
	}
	
	public Claimer(long stDisc,List<BluetoothDevice> mNewBlDvcs,Context cntxt, Activity actvty){
		startDisc = stDisc;
		context = cntxt;
		activity = actvty;
		tv = (TextView) activity.findViewById(R.id.textView1);
		mNewBluetoothDevices = mNewBlDvcs;
		// Get local Bluetooth adapter
        mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
		try {
			serverAddr = InetAddress.getByName(IPAddress);
		} catch (UnknownHostException e) {
			
			e.printStackTrace();
			String info3 = "Error while server IP lookup!! \n";
			if(D) showToast(info3 + e.toString());
		}
	}
	//test method	
	public void claim(){
		
		
		
		//send bluetooth msgs to verifiers
		for(int i = 0; i< mNewBluetoothDevices.size();i++){
			if(D) showToast("Connecting to: "+mNewBluetoothDevices.get(i).getName());
			if(mNewBluetoothDevices.get(i).getName().contains("DROID"))
				connect(mNewBluetoothDevices.get(i));
		}
		
		//wait for LBS coupon response
		long lbsResp1 = System.currentTimeMillis();
		recieve();
		long lbsResp2 = System.currentTimeMillis();
		long lbsResp = lbsResp2 - lbsResp1;
		if(D) showToast("lbsResp time: "+lbsResp);
		//String info = "LINK Claim accepted!!";
		//if(D) showToast(info);
		
		String times = "";//"LCA RTT: " + lca + " \r\n" +"lbsResp time: " + lbsResp + " \r\n";
		tv.append(" \r\n"+times);
	}

	//test method
	private void connect(BluetoothDevice device) {
		// TODO Auto-generated method stub
		if(D) showToast("connecting to: "+device.getAddress()+" \r\n");
		//new Thread(new ConnectThread(device,trID)).start();
		
	}

	public void showToast(String msg){
		CharSequence text = msg;
	 	   int duration = Toast.LENGTH_SHORT;

	 	   Toast toast = Toast.makeText(context, text, duration);
	 	   toast.show();
	}
	
	public void sendToLBS(){
		String info = "Sending to LBS!!";
		if(D) showToast(info);
		//Server IP: 10.1.170.85
		//String IPAddress = "10.1.170.85";
		//String IPAddress = "manoop.dyndns.org";
		//String IPAddress = "173.3.201.174";
		int port =9000;
		try
		{
			System.out.println("Client socket connecting... ");
			if(D) showToast("Client socket connecting... ");
			// Create a Client socket bound to a server at IPAddress Port 8000
			//InetAddress serverAddr = InetAddress.getByName(IPAddress);

			Socket clientSocket = new Socket(serverAddr,port);

			System.out.println("Client socket connected... ");
			if(D) showToast("Client socket connected... ");
			// Create an output stream to send the message to server
			PrintStream outputStream=new PrintStream(clientSocket.getOutputStream());
			String msg = "claim: My current Location \n";
			outputStream.print(msg);
			
			outputStream.flush();

			// Create an input stream to recieve server's response
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			
			String response = inputStream.readLine();
			System.out.println("Response from Server : "+response);

			if(D) showToast("LBS response: "+response);
			
			// Close the input and output streams
			inputStream.close();
			outputStream.close(); 

			clientSocket.close();
		}
		
		
		// Catch any exception in execution of the above statements and notify user
		catch(Exception e)
		{
			System.err.println(e);
			String info3 = "Error while sending msg to LBS!! \n";
			if(D) showToast(info3 + e.toString());
		}	
	}
	
	public void sendToLCA(String id){
		String info = "Sending to LCA!!";
		if(D) showToast(info);
		//Server IP: 10.1.170.85
		
		int port =8000;
		try
		{
			System.out.println("Client socket connecting... ");
			if(D) showToast("Client socket connecting... ");
			// Create a Client socket bound to a server at IPAddress Port 8000
			//InetAddress serverAddr = InetAddress.getByName(IPAddress);

			Socket clientSocket = new Socket(serverAddr,port);

			System.out.println("Client socket connected... ");
			if(D) showToast("Client socket connected... ");
			// Create an output stream to send the message to server
			PrintStream outputStream=new PrintStream(clientSocket.getOutputStream());
			//Below temp holders replaced by real data from phone
			//String id = "1";
			String loc = "12";
			String vrfrAddresses ="";
			for(int i=0;i<mNewBluetoothDevices.size();i++)
				vrfrAddresses = vrfrAddresses+mNewBluetoothDevices.get(i).getAddress()+",";
			String msg = "claim," + id + "," + loc + ",999,0," + clientSocket.getLocalAddress() + "," + vrfrAddresses + " \n";
			outputStream.print(msg);
			
			outputStream.flush();

			// Create an input stream to recieve server's response
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			
			String response = inputStream.readLine();
			Scanner sc = new Scanner(response).useDelimiter(",");
			trID = sc.next();
			System.out.println("Response from Server : "+trID);

			if(D) showToast("LCA response: "+trID+" for claimerid: "+id);
			
			// Close the input and output streams
			inputStream.close();
			outputStream.close(); 

			clientSocket.close();
		}
		
		
		// Catch any exception in execution of the above statements and notify user
		catch(Exception e)
		{
			System.err.println(e);
			String info3 = "Error while sending msg to LCA!! \n";
			if(D) showToast(info3 + e.toString());
		}	
	}
	
	public void recieve(){
		try
		{
			int clientNumber = 0;
			// Create a Server Socket at local host on the Port 8001
			ServerSocket serverSocket = new ServerSocket(8001);
			System.out.println("This is a Server at ");
			
			// Notify the IP Address and Port Number to the user
			System.out.println("IP Address		: " +((serverSocket.getInetAddress()).getLocalHost()).getHostAddress());
			System.out.println("Port Number		: 8001");

			// Accept response from LBS
	
			System.out.println("\nWaiting for a LBS...");

			// Accept a Client Socket
			Socket clientSocket=serverSocket.accept();
			String clientsMessage = "";

			// Create an input stream to read a message from the Client Socket
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			
			// Create an output stream to send response to the Client Socket
			PrintStream outputStream = new PrintStream(clientSocket.getOutputStream());
			
			// Read message sent by client
			clientsMessage = inputStream.readLine();

			if(D) showToast("LBS Response: "+clientsMessage);
			tv.append("LBS Response: "+clientsMessage+" \r\n");	
			outputStream.flush();
			outputStream.close();
			inputStream.close();
			
			serverSocket.close();
		
		}

		// Catch any exception in execution of the above statements and notify user
		catch(Exception e)
		{
			System.err.println(e);
			String info4 = "Error while listening to LBS response!! \n";
			if(D) showToast(info4 + e.toString());
		}
	}
	
	public void verificationToLCA(String id, String trID) {

		int port = 8000;
		try {
			System.out.println("Client socket connecting... ");

			// Create a Client socket bound to a server at IPAddress Port 8000
			InetAddress serverAddr = InetAddress.getByName(IPAddress);

			Socket clientSocket = new Socket(serverAddr, port);

			System.out.println("Client socket connected... ");

			// Create an output stream to send the message to server
			PrintStream outputStream = new PrintStream(clientSocket
					.getOutputStream());
			// Below temp holders replaced by real data from phone
			//String id = "1";
			//String trID = "1";
			String claimerIP = "";// Just a blank holder for now. Not used
									// currently.
			String msg = "verification," + trID.trim() + "," + id.trim()
					+ ",12,999,0," + claimerIP.trim() + " \n";
			outputStream.print(msg);

			outputStream.flush();

			// Create an input stream to recieve server's response
			BufferedReader inputStream = new BufferedReader(
					new InputStreamReader(clientSocket.getInputStream()));

			String response = inputStream.readLine();
			System.out.println("Response from Server : " + response);

			// Close the input and output streams
			inputStream.close();
			outputStream.close();

			clientSocket.close();
		}

		// Catch any exception in execution of the above statements and notify
		// user
		catch (Exception e) {
			String info3 = "Error while sending msg to LCA!! \n";
			System.err.println(info3 + e);

		}
	}
	//test method
	public void sendToServer(){
		String info = "Sending to LBS!!";
		if(D) showToast(info);
		//Server IP: 10.1.170.85
		//String IPAddress = "10.1.170.85";
		//String IPAddress = "manoop.dyndns.org";
		//String IPAddress = "173.3.201.174";
		int port =9000;
		try
		{
			System.out.println("Client socket connecting... ");
			if(D) showToast("Client socket connecting... ");
			// Create a Client socket bound to a server at IPAddress Port 8000
			//InetAddress serverAddr = InetAddress.getByName(IPAddress);

			Socket clientSocket = new Socket(serverAddr,port);

			System.out.println("Client socket connected... ");
			if(D) showToast("Client socket connected... ");
			// Create an output stream to send the message to server
			PrintStream outputStream=new PrintStream(clientSocket.getOutputStream());
			String msg = "claim: My current Location \n";
			DigitalSignature ds = new DigitalSignature();
			String del = "manoop";
			byte [] signature = ds.sign(msg, ds.rsaPrivkey);
			String myString = Base64.encodeToString(signature, 0);
			//String myString = new String(signature);
//			outputStream.write(ds.rsaPubkey.getModulus().toByteArray());
//			outputStream.write(del.getBytes());
//			outputStream.write(ds.rsaPubkey.getPublicExponent().toByteArray());
//			outputStream.write(del.getBytes());
//			outputStream.write(signature);
//			outputStream.write(del.getBytes());
//			outputStream.write(msg.getBytes());
//			outputStream.write(del.getBytes());
			outputStream.print(ds.rsaPubkey.getModulus().toString()+"manoop"+ds.rsaPubkey.getPublicExponent().toString()+"manoop"+myString+"manoop"+msg+"manoop");
			
			outputStream.flush();

			// Create an input stream to recieve server's response
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
			
			String response = inputStream.readLine();
			System.out.println("Response from Server : "+response);

			if(D) showToast("LBS response: "+response);
			
			// Close the input and output streams
			inputStream.close();
			outputStream.close(); 

			clientSocket.close();
		}
		
		
		// Catch any exception in execution of the above statements and notify user
		catch(Exception e)
		{
			System.err.println(e);
			String info3 = "Error while sending msg to LBS!! \n";
			if(D) showToast(info3 + e.toString());
		}	
	}
}
