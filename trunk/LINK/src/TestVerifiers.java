import java.io.IOException;
import java.io.PrintStream;
import java.net.Socket;
import java.net.UnknownHostException;


public class TestVerifiers {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		Socket mob;
		int i = 2;
		while(i<5){
			try {
				mob = new Socket("192.168.1.7",8000);
				PrintStream lbsStream=new PrintStream(mob.getOutputStream());
				int loc = 12;
				if(i < 4){
					loc = 13;
				}
				lbsStream.print("verification,0,"+i+++","+loc+",999,0");
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

}
