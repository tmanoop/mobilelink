/* 
 * Program : Server.java
 * 
 * Description :
 * 
 * This is a Server program that displays messages sent by Clients processes and sends a 
 * response "Message Recieved" to the corresponding Client Sockets.
 * 
 * Any number of Client Sockets are accepted in a serial fashion. Only one message from a 
 * client is  accpeted. The server process kills itself once a message "kill" (case insensitive) 
 * is recieved from the Client Socket it is currently bound to.
 * 
 * The Client process created by the program Client.java can be handled by this Server.
 *  
 */

import java.net.*;
import java.util.Scanner;
import java.io.*;

public class LBSServer
{
	// Server Object constructor
	public LBSServer()
	{
		try
		{
			int clientNumber = 0;
			// Create a Server Socket at local host on the Port 8000
			ServerSocket serverSocket = new ServerSocket(9000);
			System.out.println("This is a Server at ");
			
			// Notify the IP Address and Port Number to the user
			System.out.println("IP Address		: " +((serverSocket.getInetAddress()).getLocalHost()).getHostAddress());
			System.out.println("Port Number		: 9000");

			// Accept clients one after another
			while(true)
			{

				System.out.println("\nWaiting for a Client...");

				// Accept a Client Socket
				Socket clientSocket=serverSocket.accept();
				String clientsMessage;

				// Create an input stream to read a message from the Client Socket
				BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
				
				// Create an output stream to send response to the Client Socket
				PrintStream outputStream = new PrintStream(clientSocket.getOutputStream());
				
				// Read message sent by client
				clientsMessage = inputStream.readLine();

				// Check if the message is not "kill" and act accordingly
				if(!clientsMessage.equalsIgnoreCase("kill"))
				{
					
					
					if(clientsMessage.contains("LCA Decision")){
						//Scanner s = new Scanner(clientsMessage);
						System.out.println("Message from LCA : " + clientsMessage);
						//int userID = s.nextInt();
						//find out how to connect to mobile user using his id
						//And then send below message.
						String msg = "Coupon sent to claimer!! \r\n";
						String MOBIP = clientsMessage.substring(0, clientsMessage.indexOf("ClientIP") - 1).trim();
						//String MOBIP = "";
						System.out.println("MOBIP: "+MOBIP);
						//sometimes mobile client listener is not yet ready. so keeping buffer time 3secs
						Thread.currentThread().sleep(3000);//sleep for 1000 ms
						Socket mobClient = new Socket(MOBIP,8000);
						PrintStream mobStream=new PrintStream(mobClient.getOutputStream());
						mobStream.print(msg);
						mobStream.flush();
						mobStream.close();
						mobClient.close();
						
						System.out.println(msg);
					}
					//if claimer, then reply back requesting location verification. "verify your location at LCA"
					else if(clientsMessage.contains("claim")){
						System.out.println("Message from Client " + clientNumber + ": " + clientsMessage);
						String msg = "verify your location at LCA.";
						outputStream.print(msg);
						//Socket mobSocket = new Socket("128.235.69.143",8001);
						//PrintStream mobStream=new PrintStream(clientSocket.getOutputStream());
						//mobStream.print(msg);
					}
					//if LCA, then process the service request as per the decision.
					
						
					// Send response to client
					
					
					outputStream.flush();
					outputStream.close();
					inputStream.close();
					
					
				}
				
					// If the message is "kill" 
				else
				{
					System.out.println("Killer Client Connected");

					// Send response to client
					outputStream.print("Server process being killed...");
					System.out.println("Server process being killed...");

					outputStream.flush();
					outputStream.close();
					inputStream.close();

					serverSocket.close();

					System.exit(0);
				}
			}	
		}

		// Catch any exception in execution of the above statements and notify user
		catch(Exception e)
		{
			System.err.println(e);
		}
	}
	
	public static void main(String args[])
	{
	// Create a new Server Object
	new LBSServer();
	}
	
}