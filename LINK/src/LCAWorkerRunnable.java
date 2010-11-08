import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Date;
import java.util.Scanner;

/**

 */
public class LCAWorkerRunnable implements Runnable{

    protected Socket clientSocket = null;
    protected LCA lca   = null;
    protected String LBSIP = null;

    public LCAWorkerRunnable(Socket clientSocket, LCA lca, String LBSIP) {
        this.clientSocket = clientSocket;
        this.lca   = lca;
        this.LBSIP = LBSIP;
    }

    public void run() {
        try {
            InputStream input  = clientSocket.getInputStream();
            OutputStream output = clientSocket.getOutputStream();
            //long time = System.currentTimeMillis();
          
//            output.write(("HTTP/1.1 200 OK\n\nWorkerRunnable: " +
//                    this.serverText + " - " +
//                    time +
//                    "").getBytes());
//            output.write((new Date()+" - Message Recieved!!").getBytes());
            LCAProcess(input,output);
            output.close();
            input.close();
            System.out.println("Request processed at " + new Date());
        } catch (IOException e) {
            //report exception somewhere.
            e.printStackTrace();
        }
    }
    
    @SuppressWarnings("static-access")
	public void LCAProcess(InputStream input, OutputStream output){

		try {
			// Create an input stream to read a message from the Client Socket
			BufferedReader inputStream= new BufferedReader(new InputStreamReader(input));
			
			// Create an output stream to send response to the Client Socket
			PrintStream outputStream = new PrintStream(output);
			
			String clientsMessage = inputStream.readLine();
			if(clientsMessage.contains("My Location")){
				System.out.println("Message from Client: " + clientsMessage);
				String msg = "claimer's location request recieved!! ";
				System.out.println(msg);
				
				outputStream.print("LCA recieved claim!! \r\n");
				//mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + 0.1;
				//then send to LBS that claimer location is correct
				//Client lcaClient = new Client("192.168.1.6",msg,true,9000);
				//Socket lbsSocket = new Socket(LBSIP,9000);
//			PrintStream lbsStream=new PrintStream(lbsSocket.getOutputStream());
//			lbsStream.print(msg);
//			lbsStream.flush();
//			lbsStream.close();
//			lbsSocket.close();
				//mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + 0.1;
				//then send to LBS that claimer location is correct
				//Client lcaClient = new Client("LBS IP",msg,true);
			} else if(clientsMessage.contains("Claimer Location")) {
				System.out.println("Message from verifier: " + clientsMessage);
				String MOBIP = clientsMessage.substring(0, clientsMessage.indexOf("ClientIP") - 1).trim();
				//String MOBIP = clientSocket.getInetAddress().toString();
				String msg = MOBIP + " ClientIP - LCA Decision: Claim Approved.";
				System.out.println(msg+" sending to LBS!!");
				
				outputStream.print("LCA recieved claim!! \n");
				//mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + 0.1;
				//then send to LBS that claimer location is correct
				//Client lcaClient = new Client("192.168.1.6",msg,true,9000);
				Socket lbsSocket = new Socket(LBSIP,9000);
				PrintStream lbsStream=new PrintStream(lbsSocket.getOutputStream());
				lbsStream.print(msg);
				lbsStream.flush();
				lbsStream.close();
				lbsSocket.close();
			}
			
			//handle claims
			if(clientsMessage.contains("claim")){
				User c = new User();
				//set user info in this claimer instance
				//claimer msg example of clientsMessage = claim: 01,12,999,0
				Scanner s = new Scanner(clientsMessage).useDelimiter(",");
				s.next();
				c.id = s.nextInt();
				c.location = s.nextInt();
				c.serviceID = s.nextInt();
				c.time = s.nextInt();
				int tr_id = lca.transactions.size();
				Transaction tr = new Transaction(tr_id);
				tr.c = c;
				lca.transactions.add(tr);
				lca.mobileUsers.get(c.id).totalCount++;
				outputStream.print(tr_id+"  tr_id. LCA recieved claim!! \n");
				try {
					System.out.println(tr_id+"  tr_id. LCA waiting for verifications...");
					Thread.currentThread().sleep(20000);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}//sleep for 1000 ms
				//String MOBIP = lca.transactions.get(tr_id).c.MOBIP;
				String MOBIP = clientSocket.getInetAddress().toString();
				if(lca.decisionProcess(lca.transactions.get(tr_id).c) == 1)
				{
					String msg = MOBIP + " ClientIP - LCA Decision: Claim Approved for user "+c.id;
					System.out.println(tr_id+"  tr_id. "+msg);
					//mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + 0.1;
					//then send to LBS that claimer location is correct
					//Client lcaClient = new Client("LBS IP",msg,true,9000);
					Socket lbsSocket = new Socket(LBSIP,9000);
					PrintStream lbsStream=new PrintStream(lbsSocket.getOutputStream());
					lbsStream.print(msg);
					lbsStream.flush();
					lbsStream.close();
					lbsSocket.close();
				} else
				{
					String msg = MOBIP + " ClientIP - LCA Decision: Claim Rejected for user "+c.id;
					System.out.println(tr_id+"  tr_id. "+msg);
					//his claim is rejected
					//Client lcaClient = new Client("LBS IP",msg,true,9000);
					Socket lbsSocket = new Socket(LBSIP,9000);
					PrintStream lbsStream=new PrintStream(lbsSocket.getOutputStream());
					lbsStream.print(msg);
					lbsStream.flush();
					lbsStream.close();
					lbsSocket.close();
				}
				//flush the verifiers for this claimer
				//mobileUsers.get(c.id).V.clear();
				System.out.print(tr_id+"  tr_id. ");
			}
			
			//handle verifications
			if(clientsMessage.contains("verification")){
				//verifier msg example of clientsMessage = verification: claimer id,01,12,999,0
				Scanner s = new Scanner(clientsMessage).useDelimiter(",");
				s.next();
				int tr_id = s.nextInt();
				User claimer = lca.transactions.get(tr_id).c;
				//set user info in this claimer instance
				//claimer msg example of clientsMessage = 01,12,999,0
				User vrfr = new User();
				
				vrfr.id = s.nextInt();
				vrfr.location = s.nextInt();
				vrfr.serviceID = s.nextInt();
				vrfr.time = s.nextInt();
				claimer.MOBIP = s.next();				
				claimer.V.add(vrfr);
				
				System.out.print(tr_id+"  tr_id. ");
			}
			
			// Send response to client
			outputStream.print("Message Recieved...");
			
			outputStream.flush();
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	
    }
}