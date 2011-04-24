package com.example.helloandroid;

import java.io.File;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.util.List;

import android.os.Environment;

public class FileAccess {

	public static void writeToFile(List<String> data){
		String FILENAME = "LINKBench.txt";
		//String string = "hello world! \r\n hello link!!";

		File path = Environment.getExternalStoragePublicDirectory(
	            Environment.DIRECTORY_DOWNLOADS);
	    File file = new File(path, FILENAME);
		
		FileOutputStream fos;
		try {
			FileWriter fw = new FileWriter(file, true);
			//fos = new FileOutputStream(file);
			//fos = openFileOutput(FILENAME, Context.MODE_WORLD_WRITEABLE);
			for(int i =0;i<data.size();i++){
				fw.write(data.get(i));
				//fos.write(data.get(i).getBytes());
			}
			fw.close();
			//fos.close();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
    
	
}
