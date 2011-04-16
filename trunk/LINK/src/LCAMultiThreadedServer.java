import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Scanner;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintStream;

public class LCAMultiThreadedServer implements Runnable{

    protected int          serverPort   = 8080;
    protected ServerSocket serverSocket = null;
    protected boolean      isStopped    = false;
    protected Thread       runningThread= null;
    protected int userSize = 10;
    protected ArrayList<User> mobileUsers;
    protected int[][] M;
    protected LCA lca;
    protected String LBSIP;
	
    public LCAMultiThreadedServer(int port){
        this.serverPort = port;
        this.lca = new LCA(userSize);
    }

    public void run(){
        synchronized(this){
            this.runningThread = Thread.currentThread();
        }
        openServerSocket();
        while(! isStopped()){
            Socket clientSocket = null;
            try {
                clientSocket = this.serverSocket.accept();
            } catch (IOException e) {
                if(isStopped()) {
                    System.out.println("Server Stopped.") ;
                    return;
                }
                throw new RuntimeException(
                    "Error accepting client connection", e);
            }
            
            try {
				// Create an input stream to read a message from the Client Socket
				BufferedReader inputStream= new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
				
				// Create an output stream to send response to the Client Socket
				PrintStream outputStream = new PrintStream(clientSocket.getOutputStream());
				
				String clientsMessage = inputStream.readLine();
				//check claim/verification
				
				//if claim create LCAWorkerRunnable object
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
					
					c.setMOBIP(s.next()); //this didnot work when testing in NJIT network, use below line
					//s.next();
					//c.setMOBIP(clientSocket.getInetAddress().toString());
					List<String> verifiersAddressList = new ArrayList<String>();
					String validAddresses ="";
					while(s.hasNext()){
						String address = s.next();
						if(lca.isValidVerifier(address)){
							verifiersAddressList.add(address);
							validAddresses = validAddresses + address + ",";
						}
					}
					
					int tr_id = lca.tr_id++;
					Transaction tr = new Transaction(tr_id);
					tr.verifiersCount = verifiersAddressList.size();
					
					
					tr.c = c;
					//read data and store in lca
					//create transaction and store object in map 
					lca.transactions.put(tr_id,tr);
					lca.mobileUsers.get(c.id).totalCount++;
					//write tr_id to claimer
					//System.out.println(tr_id+"  tr_id. "+validAddresses);
					outputStream.print(tr_id+" ,"+validAddresses);
					outputStream.flush();
					//new thread on LCAWorkerRunnable object and start it
					new Thread(
					        new LCAWorkerRunnable(
					            tr_id, this.lca, LBSIP)
					    ).start();
				}
				
				//if verification read data and notify
				//handle verifications
				else if(clientsMessage.contains("verification")){
					//verifier msg example of clientsMessage = verification: claimer id,01,12,999,0
					Scanner s = new Scanner(clientsMessage).useDelimiter(",");
					s.next();
					int tr_id = s.nextInt();
					Transaction parent = lca.transactions.get(tr_id);
					synchronized(parent)
					{
						User claimer = lca.transactions.get(tr_id).c;
						//set user info in this claimer instance
						//claimer msg example of clientsMessage = 01,12,999,0
						User vrfr = new User();
						
						vrfr.id = s.nextInt();
						vrfr.location = s.nextInt();
						vrfr.serviceID = s.nextInt();
						vrfr.time = s.nextInt();
						//claimer.setMOBIP(s.next());				
						claimer.V.add(vrfr);
						
						parent.notify();
						
						long elapsedTimeMillis = System.currentTimeMillis()-parent.prevReqTime; 
						System.out.println(tr_id+"  tr_id. After "+elapsedTimeMillis+"msecs verifierID:"+vrfr.id+" at "+new Date());
						parent.prevReqTime = System.currentTimeMillis();
					}
				} else if(clientsMessage.contains("verifiersCount")){
					Scanner s = new Scanner(clientsMessage).useDelimiter(",");
					s.next();
					int tr_id = s.nextInt();
					Transaction parent = lca.transactions.get(tr_id);
					synchronized(parent)
					{
						parent.verifiersCount = s.nextInt();
						parent.notify();
					}
					System.out.println(tr_id+"  tr_id. Updated verifiers count to: "+parent.verifiersCount+" at "+new Date());
				}
				else
					System.out.println("Request recieved at " + new Date());
				inputStream.close();
				outputStream.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
            
        }
        System.out.println("Server Stopped.") ;
    }


    private synchronized boolean isStopped() {
        return this.isStopped;
    }

    public synchronized void stop(){
        this.isStopped = true;
        try {
            this.serverSocket.close();
        } catch (IOException e) {
            throw new RuntimeException("Error closing server", e);
        }
    }

    private void openServerSocket() {
        try {
            this.serverSocket = new ServerSocket(this.serverPort);
            System.out.println("This is a Server at ");
    		
    		// Notify the IP Address and Port Number to the user
            LBSIP = ((serverSocket.getInetAddress()).getLocalHost()).getHostAddress();
    		System.out.println("IP Address		: " +LBSIP);
    		System.out.println("Port Number		: "+serverPort);
        } catch (IOException e) {
            throw new RuntimeException("Cannot open port 8080", e);
        }
    }

    public static void main(String[] args){
    	LCAMultiThreadedServer server = new LCAMultiThreadedServer(8000);
		
		try {
			new Thread(server).start();
//		    Thread.sleep(20 * 1000);
		} catch (Exception e) {
		    e.printStackTrace();
		}
//		System.out.println("Stopping Server");
//		server.stop();
    }
}