
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.text.DecimalFormat;

public class LogParser {

	/**
	 * @param args
	 */
	@SuppressWarnings("deprecation")
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println("Start Log Parsing..");
		if(args.length != 0) {
			File file = new File(args[0]);
		    FileInputStream fis = null;
		    BufferedInputStream bis = null;
		    DataInputStream dis = null;
	
		    try {
		      fis = new FileInputStream(file);
	
		      // Here BufferedInputStream is added for fast reading.
		      bis = new BufferedInputStream(fis);
		      dis = new DataInputStream(bis);
		      
		      int totFalseClaims = 0;
		      int totRightClaims = 0;
		      int aprFalseClaims = 0;
		      int rejRightClaims = 0;
		      
		      float falseNegativeRate = 0;
		      float falsePositiveRate = 0;
		      // dis.available() returns 0 if the file does not have more lines.
		      while (dis.available() != 0) {
	
		      // this statement reads the line from the file and print it to
		        // the console.
		        //System.out.println(dis.readLine());
		    	String line = dis.readLine();
		        if(line.startsWith("False claim rejected") || line.startsWith("False claim ignored")) {
		        	totFalseClaims++;
		        }
		        if(line.startsWith("False claim approved")) {
		        	totFalseClaims++;
		        	aprFalseClaims++;
		        }
		        if(line.startsWith("True claim rejected") || line.startsWith("True claim ignored")) {
		        	totRightClaims++;
		        	rejRightClaims++;
		        }
		        if(line.startsWith("True claim approved")) {
		        	totRightClaims++;
		        }
		      }
		      
		DecimalFormat df = new DecimalFormat("#.#####");

		      if(totFalseClaims != 0)
		    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
		      if(totRightClaims != 0)
		    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;

		      System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
		      
		      System.out.println("falseNegativeRate: "+df.format(falseNegativeRate)+" falsePositiveRate: "+df.format(falsePositiveRate));
		      // dispose all the resources after using them.
		      fis.close();
		      bis.close();
		      dis.close();
	
		    } catch (FileNotFoundException e) {
		      e.printStackTrace();
		    } catch (IOException e) {
		      e.printStackTrace();
		    }
		} else {
			System.out.println("No input log file location!!");
		}
		System.out.println("End Log Parsing..");
	}

}
