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

import java.math.BigInteger;
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
				InputStream is = clientSocket.getInputStream();
				InputStream temp = is;
				BufferedReader inputStream= new BufferedReader(new InputStreamReader(is));
				
				// Create an output stream to send response to the Client Socket
				PrintStream outputStream = new PrintStream(clientSocket.getOutputStream());
				Scanner sc = new Scanner(temp).useDelimiter("Manoop");
				
				// Read message sent by client
				//clientsMessage = inputStream.readLine();
				byte[] m = sc.next().getBytes();
				
				System.out.println("m: "+new String(m));
				
				//clientsMessage = inputStream.readLine();
				byte[] exp = sc.next().getBytes();
				
				System.out.println("exp: "+new String(exp));
				
				//clientsMessage = inputStream.readLine();
				byte[] s = sc.next().getBytes();
			
				System.out.println("s: "+new String(s));
				
				//clientsMessage = inputStream.readLine();
				byte[] org = sc.next().getBytes();
			
				System.out.println("org: "+new String(org));
				
				DigitalSignature ds = new DigitalSignature();
				
				BigInteger mod = new BigInteger(new String(m));
				BigInteger e = new BigInteger(new String(exp));
				
				RSAPublicKey rsaPubkey = (RSAPublicKey)ds.readPublicKey(mod,e);
				
				System.out.println(rsaPubkey);
				
				System.out.println(ds.verify(org, rsaPubkey, s));
				
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