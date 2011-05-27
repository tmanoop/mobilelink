/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Administrator
 */
public class ParseFiles
{

    public ParseFiles(String fileName)
    {
        File outCPU = new File("cpu.txt");
        File outWiFi = new File("wifi.txt");
        File outLCD = new File("lcd.txt");
        File outtot = new File("total.txt");
        String process_id = "10096";			//use your processID here
        try
        {
            File file = new File(fileName);
            BufferedReader br = new BufferedReader(new FileReader(file));

            String ind="";
            
            PrintWriter pwCPU = new PrintWriter(outCPU);
            PrintWriter pwWiFi = new PrintWriter(outWiFi);
            PrintWriter pwLCD = new PrintWriter(outLCD);
            PrintWriter pwTOT = new PrintWriter(outtot);
            String st = br.readLine();
            while(st != null)
            {
            	if(st.contains("begin"))
                {
                    ind =st.substring(st.lastIndexOf(' ')+1);
                }
                if(st.contains("CPU-"+process_id))
                {
                    pwCPU.println(ind+" "+st.substring(st.lastIndexOf(' ')+1));
                }
                else if(st.contains("Wifi-"+process_id))
                {
                    pwWiFi.println(ind+" "+st.substring(st.lastIndexOf(' ')+1));
                }
                else if(st.contains("LCD-"+process_id))
                {
                    pwLCD.println(st.substring(st.lastIndexOf(' ')+1));
                }
                else if(st.contains("total-power"))
                {
                	String x= ind+" "+st.substring(st.lastIndexOf(' ')+1);
                    pwTOT.println(x);
                }
                st = br.readLine();
            }
            pwCPU.flush();
            pwWiFi.flush();
            pwLCD.flush();
            pwTOT.flush();
            System.out.println("Done....");
        }
        catch (IOException ex)
        {
            Logger.getLogger(ParseFiles.class.getName()).log(Level.SEVERE, null, ex);
        }
       
    }
    
    public static void main(String[] args) {
		try {
			String fileName = "BLConn\\V1PowerTrace1304304504832";
			String PATH_OF_YOUR_LOG_FILE = "C:\\Manoop\\njit\\LINK_journal_work\\Android_TestResults\\"+fileName+".log";
			ParseFiles parseFiles = new ParseFiles(PATH_OF_YOUR_LOG_FILE);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	}

}
