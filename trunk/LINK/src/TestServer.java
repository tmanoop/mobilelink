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
import java.security.interfaces.RSAPublicKey;
import java.util.Scanner;
import java.io.*;

public class TestServer
{
	// Server Object constructor
	public TestServer()
	{
		try
		{
			
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
				byte[] m = clientsMessage.getBytes();
				
				System.out.println("m: "+new String(m));
				
				clientsMessage = inputStream.readLine();
				byte[] exp = clientsMessage.getBytes();
				
				System.out.println("exp: "+new String(exp));
				
				clientsMessage = inputStream.readLine();
				byte[] s = clientsMessage.getBytes();
			
				System.out.println("s: "+new String(s));
				
				DigitalSignature ds = new DigitalSignature();
				RSAPublicKey rsaPubkey = (RSAPublicKey)ds.readPublicKey(new String(m), new String(exp));
				
				System.out.println(ds.verify("claim: 01,12,999,0", rsaPubkey, s));
				
				outputStream.flush();
				outputStream.close();
				inputStream.close();
			
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
	new TestServer();
	}
	
}