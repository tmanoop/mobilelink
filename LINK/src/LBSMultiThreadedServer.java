import java.net.ServerSocket;
import java.net.Socket;
import java.io.IOException;

public class LBSMultiThreadedServer implements Runnable{

    protected int          serverPort   = 8080;
    protected ServerSocket serverSocket = null;
    protected boolean      isStopped    = false;
    protected Thread       runningThread= null;
    static int threadCount = 0; 
    protected String LCAIP;

    public LBSMultiThreadedServer(int port){
        this.serverPort = port;
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
            System.out.println("Thread "+threadCount++) ;
            new Thread(
                new LBSWorkerRunnable(
                    clientSocket, "Thread "+threadCount, LCAIP)
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
    		
            LCAIP = ((serverSocket.getInetAddress()).getLocalHost()).getHostAddress();
    		// Notify the IP Address and Port Number to the user
    		System.out.println("IP Address		: " +LCAIP);
    		System.out.println("Port Number		: "+serverPort);
        } catch (IOException e) {
            throw new RuntimeException("Cannot open port "+serverPort, e);
        }
    }

    public static void main(String[] args){
    	LBSMultiThreadedServer server = new LBSMultiThreadedServer(9000);
		
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