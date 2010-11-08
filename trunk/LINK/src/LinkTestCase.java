import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;


public class LinkTestCase {
	
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		test2();

	}

	public static void test(){
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
	
	public static void test1(){
		System.out.println("Start LCA decision");
		int size = 10;
		LCA lca = new LCA(size);
		User c = new User();
		//01,12,999,0
		c.id = 1;
		c.location = 12;
		c.serviceID = 999;
		c.time = 0;
		
		User vrfr = new User();		
		vrfr.id = 2;
		vrfr.location = 12;
		vrfr.serviceID = 999;
		vrfr.time = 0;
		
		c.V.add(vrfr);
		
		int decision = lca.decisionProcess(c);
		if(decision == 1){
			System.out.println("Claim Accepted");
		} else if(decision == -1){
			System.out.println("Claim Rejected");
		} else{
			System.out.println("Claim Ignored");
		}
		
		
		System.out.println("End LCA decision");
	}
	
	public static void test2(){
		Socket mob;
		try {
			mob = new Socket("192.168.1.7",8000);
			PrintStream lbsStream=new PrintStream(mob.getOutputStream());
			lbsStream.print("claim,1,12,999,0");
			lbsStream.flush();			
			lbsStream.close();
			mob.close();
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
