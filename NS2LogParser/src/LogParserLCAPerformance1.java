
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.text.DecimalFormat;

public class LogParserLCAPerformance1 {

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
		      
		      float falseRateAt10min = 0;
		      float falseRateAt20min = 0;
		      float falseRateAt30min = 0;
		      float falseRateAt40min = 0;
		      float falseRateAt50min = 0;
		      float falseRateAt60min = 0;
		      // dis.available() returns 0 if the file does not have more lines.
		      //float runTime = 0;
		      DecimalFormat df = new DecimalFormat("#.#####");
		      
		      boolean t10 = true;
		      boolean t20 = true;
		      boolean t30 = true;
		      boolean t40 = true;
		      boolean t50 = true;
		      boolean t60 = true;
		      
		      while (dis.available() != 0) {
		      // this statement reads the line from the file and print it to
		        // the console.
		        //System.out.println(dis.readLine());
		    	String line = dis.readLine();
		    	if(line.startsWith("node"))
		    			continue;
		    	int ind = line.indexOf("time:");
		    	ind = ind + 5;
		    	float time = Float.parseFloat(line.substring(ind));
		    	//System.out.println("time: "+time);
		    	if(t10 == true && time > 600){
		    		t10 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt10min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
		    	if(t20 == true && time > 1200){
		    		t20 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt20min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
		    	if(t30 == true && time > 1800){
		    		t30 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt30min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
		    	if(t40 == true && time > 2400){
		    		t40 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt40min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
		    	if(t50 == true && time > 3000){
		    		t50 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt50min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
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
		    	if(t60 == true){
		    		t60 = false;
		    		if(totFalseClaims != 0)
				    	  falseNegativeRate = (float)aprFalseClaims/totFalseClaims;
				    if(totRightClaims != 0)
				    	  falsePositiveRate = (float)rejRightClaims/totRightClaims;
				    falseRateAt60min = falseNegativeRate;
				    System.out.println("aprFalseClaims: "+aprFalseClaims+" totFalseClaims: "+totFalseClaims+" rejRightClaims: "+rejRightClaims+" totRightClaims: "+totRightClaims);
				    falseNegativeRate = 0;
				    falsePositiveRate = 0;
				    totFalseClaims = 0;
				    aprFalseClaims = 0;
				    totRightClaims = 0;
				    rejRightClaims = 0;
		    	}
		      
		      
		      System.out.println("falseRateAt10min: "+df.format(falseRateAt10min));
		      System.out.println("falseRateAt20min: "+df.format(falseRateAt20min));
		      System.out.println("falseRateAt30min: "+df.format(falseRateAt30min));
		      System.out.println("falseRateAt40min: "+df.format(falseRateAt40min));
		      System.out.println("falseRateAt50min: "+df.format(falseRateAt50min));
		      System.out.println("falseRateAt60min: "+df.format(falseRateAt60min));
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
