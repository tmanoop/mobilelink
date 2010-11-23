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

    protected int tr_id;
    protected LCA lca   = null;
    protected String LBSIP = null;
    
	private final int CUT_DOWN_BY = 8;
	

    public LCAWorkerRunnable(int tr_id, LCA lca, String LBSIP) {
        this.tr_id  = tr_id;
        this.lca   = lca;
        this.LBSIP = LBSIP;
    }

    public void run() {
        try {
            LCAProcess(tr_id);
            System.out.println("Request processed at " + new Date());
        } catch (Exception e) {
            //report exception somewhere.
            e.printStackTrace();
        }
    }
    
    public void LCAProcess(int tr_id){

		try {
				System.out.println(tr_id+"  tr_id. LCA waiting for verifications at "+ new Date());
				//Thread.currentThread().sleep(20000);
				int waitTime = 40000;
				Transaction parent = lca.transactions.get(tr_id);
				if(parent.verifiersCount == 0)
					parent.verifiersCount = 1;
				int reduceBy = waitTime/parent.verifiersCount;
				synchronized(parent)
				{
					long StartTime = System.currentTimeMillis();
					while(waitForMore(tr_id))
					{
						if(waitTime != 0){
							parent.wait(waitTime);
							if((System.currentTimeMillis() - StartTime) < waitTime )
								waitTime = waitTime/10;
							else
								break;
						}
					}
				}
				String msg="";
				User c = lca.transactions.get(tr_id).c;
				String MOBIP = c.MOBIP;
				if(lca.decisionProcess(c) == 1)
				{
					msg = MOBIP + " ClientIP - LCA Decision: Claim Approved for user "+c.id;
					System.out.println(tr_id+"  tr_id. "+msg);
				} else
				{
					msg = MOBIP + " ClientIP - LCA Decision: Claim Rejected for user "+c.id;
					System.out.println(tr_id+"  tr_id. "+msg);					
				}
				Socket lbsSocket = new Socket(LBSIP,9000);
				PrintStream lbsStream=new PrintStream(lbsSocket.getOutputStream());
				lbsStream.print(msg);
				lbsStream.flush();
				lbsStream.close();
				lbsSocket.close();
				//flush the verifiers for this claimer
				//mobileUsers.get(c.id).V.clear();
				//System.out.print(tr_id+"  tr_id. ");
		
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	
    }

	private boolean waitForMore(int tr_id) {
		Transaction t = lca.transactions.get(tr_id);
		return t.c.V.size() < t.verifiersCount;
	}
}