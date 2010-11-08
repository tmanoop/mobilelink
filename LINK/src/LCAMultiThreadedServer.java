import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.io.IOException;

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
            new Thread(
                new LCAWorkerRunnable(
                    clientSocket, this.lca, LBSIP)
            ).start();
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