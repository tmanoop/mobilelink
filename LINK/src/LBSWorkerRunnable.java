import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Date;

/**

 */
public class LBSWorkerRunnable implements Runnable{

    protected Socket clientSocket = null;
    protected String serverText   = null;
    protected String LCAIP = null;

    public LBSWorkerRunnable(Socket clientSocket, String serverText, String lcaIP) {
    	LCAIP = lcaIP;
        this.clientSocket = clientSocket;
        this.serverText   = serverText;
    }

    public void run() {
        try {
            InputStream input  = clientSocket.getInputStream();
            OutputStream output = clientSocket.getOutputStream();
            long time = System.currentTimeMillis();
//            output.write(("HTTP/1.1 200 OK\n\nWorkerRunnable: " +
//                    this.serverText + " - " +
//                    time +
//                    "").getBytes());
            LBSProcess(input,output);
            //output.write((this.serverText+new Date()+" - Message Recieved!!").getBytes());
            //Thread.currentThread().sleep(9000);//sleep for 1000 ms
            output.close();
            input.close();
            System.out.println("Request processed: " + new Date());
        } catch (Exception e) {
            //report exception somewhere.
            e.printStackTrace();
        }
    }
    
    public void LBSProcess(InputStream input, OutputStream output){
    	
    	try {
			// Create an input stream to read a message from the Client Socket
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(input));
			
			// Create an output stream to send response to the Client Socket
			PrintStream outputStream = new PrintStream(output);
			
			String clientsMessage = inputStream.readLine();		
			if(clientsMessage.contains("LCA Decision")){
				//Scanner s = new Scanner(clientsMessage);
				System.out.println("Message from LCA : " + clientsMessage);
				//int userID = s.nextInt();
				//find out how to connect to mobile user using his id
				//And then send below message.
				String msg = "";
				if(clientsMessage.contains("Claim Approved"))
					msg = "Coupon sent to claimer!! \r\n";
				else
					msg = "Coupon not available for claimer!! \r\n";
				String MOBIP = clientsMessage.substring(0, clientsMessage.indexOf("ClientIP") - 1).trim();
				
				//String MOBIP = "";
				System.out.println("MOBIP: "+MOBIP);
				//sometimes mobile client listener is not yet ready. so keeping buffer time 3secs
				//Thread.currentThread().sleep(6000);//sleep for 1000 ms
				int retry = 0;
				Socket mobClient = null;
				int retryNum = 15; 
				while(retry<retryNum){
					try {
						mobClient = new Socket(MOBIP,8001); //mobile client will be listening at port 8001 for the claim decision
						break;
					} catch (Exception e) {
						if(retry==(retryNum-1))
							break;
						System.out.println("claimer busy!!");
						//Thread.currentThread().sleep(2000);
						retry++;
					}
				}
				PrintStream mobStream=new PrintStream(mobClient.getOutputStream());
				mobStream.print(msg);
				mobStream.flush();
				mobStream.close();
				mobClient.close();
				
				System.out.println(msg);
			}
			//if claimer, then reply back requesting location verification. "verify your location at LCA"
			else if(clientsMessage.contains("claim")){
				System.out.println("Message from Client: " + clientsMessage);
				
				//Notify LCA.(which will check for Bluetooth inquiry clash 
				//and will set the start time for Bluetooth discovery)
				String lbsMsg="LBSRequest";
				
				Socket lcaSocket = new Socket(LCAIP,8000);
				PrintStream lcaStream=new PrintStream(lcaSocket.getOutputStream());
				lcaStream.print(lbsMsg);
				lcaStream.flush();
				
				String startDisc ="";
				
//				BufferedReader inputLCAStream= new BufferedReader(new InputStreamReader(lcaSocket.getInputStream()));
//				
//				String startDisc = inputLCAStream.readLine();
				
				
				String msg = "verify your location at LCA,startDiscoveryAt,"+startDisc;
				//clientSocket.getInetAddress();
				outputStream.print(msg);
				//Socket mobSocket = new Socket("128.235.69.143",8001);
				//PrintStream mobStream=new PrintStream(clientSocket.getOutputStream());
				//mobStream.print(msg);
				System.out.println("sent to mobile..");

				lcaStream.close();
				lcaSocket.close();
			}
			//if LCA, then process the service request as per the decision.
			
				
			// Send response to client
			
			
			outputStream.flush();
//			outputStream.close();
//			inputStream.close();
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
	
    }
}