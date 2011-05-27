import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;


public class PowerLogParser {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		List<Integer> CPU = new ArrayList<Integer>();
		
		try {
			Scanner sc = new Scanner(new File("C:\\Manoop\\njit\\LINK_journal_work\\Android_TestResults\\PowerTrace1303574740716.log")).useDelimiter(" ");
			String procID = "";
			while(sc.hasNext()){
				if(sc.next().contains("associate")){
					procID = sc.next();
					//System.out.println("ProcID: "+procID);
					if(sc.next().contains("com.example.helloandroid"))
						break;
				}
			}
			System.out.println("ProcID: "+procID);
			
			while(sc.hasNext()){
				if(sc.next().contains("CPU-"+procID)){
					
				}
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
