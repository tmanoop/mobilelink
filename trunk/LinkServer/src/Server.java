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
import java.util.ArrayList;
import java.io.*;

public class Server
{
	// Server Object constructor
	public Server()
	{
		ArrayList<User> mobileUsers = new ArrayList<User>();
		LCA lca = new LCA(mobileUsers);
		try
		{
			int clientNumber = 0;
			// Create a Server Socket at local host on the Port 8000
			ServerSocket serverSocket = new ServerSocket(8000);
			System.out.println("This is a Server at ");
			
			// Notify the IP Address and Port Number to the user
			System.out.println("IP Address		: " +((serverSocket.getInetAddress()).getLocalHost()).getHostAddress());
			System.out.println("Port Number		: 8000");

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
					System.out.println("Message from Client " + clientNumber + ": " + clientsMessage);

					// Send response to client
					outputStream.print("Message Recieved...");
					User c = new User();
					//set user info in this claimer instance
					c.id = 0;
					c.location = 0;
					c.serviceID = 0;
					c.time = 0;
					if(lca.decisionProcess(c) == 1){
						mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + 0.1;
						//then send to LBS that claimer location is correct
					} else{
						//his claim is rejected
					}
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
	new Server();
	}
	
}