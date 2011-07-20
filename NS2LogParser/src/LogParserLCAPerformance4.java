
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.text.DecimalFormat;

public class LogParserLCAPerformance4 {

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
		      
		      float trustScore = 0;
		      
		      int totClaims = 0;
		      // dis.available() returns 0 if the file does not have more lines.
		      //float runTime = 0;
		      DecimalFormat df = new DecimalFormat("#.#####");
		      
		      boolean t10 = true;
		      boolean t20 = true;
		      boolean t30 = true;
		      boolean t40 = true;
		      boolean t50 = true;
		      boolean t60 = true;
		      boolean t70 = true;
		      boolean t80 = true;
		      boolean t90 = true;
		      boolean t100 = true;
		      boolean t110 = true;
		      boolean t120 = true;
		      
		      float tvrfs = 0;
		      float ts10 = 0;
		      float ts20 = 0;
		      float ts30 = 0;
		      float ts40 = 0;
		      float ts50 = 0;
		      float ts60 = 0;
		      float ts70 = 0;
		      float ts80 = 0;
		      float ts90 = 0;
		      float ts100 = 0;
		      float ts110 = 0;
		      float ts120 = 0;
		      
		      while (dis.available() != 0) {
		      // this statement reads the line from the file and print it to
		        // the console.
		        //System.out.println(dis.readLine());
		    	String line = dis.readLine();
		    	if(line.startsWith("node") || line.startsWith("Node") || line.startsWith("False claim ignored"))
	    			continue;
		    	int ind = line.indexOf("time:");
		    	ind = ind + 5;
		    	float time = Float.parseFloat(line.substring(ind).trim());
		    	
		    	int ind1 = line.indexOf("for node ");
		    	ind1 = ind1 + 9;
		    	
		    	int id = Integer.parseInt(line.substring(ind1, ind1+2).trim());
		    	
		    	if(id>4) {
			    	
			    	//System.out.println("trustScore: "+df.format(trustScore));
			    	int ind2 = line.indexOf("NumVrfrs:");
			    	ind2 = ind2+10;
			    	int totalVrfrs = Integer.parseInt(line.substring(ind2, ind2+1).trim());
			    	//System.out.println("trustScore: "+trustScore);
			    	if(t10 == true && time > 600){
			    		t10 = false;
			    		//System.out.println("ts: "+df.format(ts)+" totClaims: "+df.format(totClaims));
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;

					    System.out.println("Avg ts10: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t20 == true && time > 1200){
			    		t20 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts20 = tvrfs;
					    System.out.println("Avg ts20: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t30 == true && time > 1800){
			    		t30 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts30 = tvrfs;
					    System.out.println("Avg ts30: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t40 == true && time > 2400){
			    		t40 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts40 = tvrfs;
					    System.out.println("Avg ts40: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t50 == true && time > 3000){
			    		t50 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts50 = tvrfs;
					    System.out.println("Avg ts50: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t60 == true && time > 3600){
			    		t60 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts60 = tvrfs;
					    System.out.println("Avg ts60: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t70 == true && time > 4200){
			    		t70 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts70 = tvrfs;
					    System.out.println("Avg ts70: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t80 == true && time > 4800){
			    		t80 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts80 = tvrfs;
					    System.out.println("Avg ts80: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t90 == true && time > 5400){
			    		t90 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts90 = tvrfs;
					    System.out.println("Avg ts90: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t100 == true && time > 6000){
			    		t100 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts100 = tvrfs;
					    System.out.println("Avg ts100: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	if(t110 == true && time > 6600){
			    		t110 = false;
			    		if(totClaims !=0)
			    			tvrfs = tvrfs/totClaims;
			    		ts110 = tvrfs;
					    System.out.println("Avg ts110: "+df.format(tvrfs));
					    tvrfs = 0;
					    totClaims = 0;
			    	}
			    	totClaims++;
			        tvrfs = tvrfs + totalVrfrs;
		    	}
		      }
		    	if(t120 == true){
		    		t120 = false;
		    		if(totClaims !=0)
		    			tvrfs = tvrfs/totClaims;
		    		ts120 = tvrfs;
				    System.out.println("Avg ts120: "+df.format(tvrfs));
				    tvrfs = 0;
				    totClaims = 0;
		    	}
		      
		      /*
		      System.out.println("falseRateAt10min: "+df.format(falseRateAt10min));
		      System.out.println("falseRateAt20min: "+df.format(falseRateAt20min));
		      System.out.println("falseRateAt30min: "+df.format(falseRateAt30min));
		      System.out.println("falseRateAt40min: "+df.format(falseRateAt40min));
		      System.out.println("falseRateAt50min: "+df.format(falseRateAt50min));
		      System.out.println("falseRateAt60min: "+df.format(falseRateAt60min));
		      */
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
