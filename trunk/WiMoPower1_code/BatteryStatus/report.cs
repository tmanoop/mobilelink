using System;
using System.Collections.Generic;
using System.IO;

public class report
{
    public report()
	{
	}
    public static void writeToFile(List<PowerStat> powerStatList)
    {
        String full_path = System.Reflection.Assembly.GetCallingAssembly().GetName().CodeBase;
        String directory_path = full_path.Substring(0, full_path.LastIndexOf("\\"));
        File.Delete(directory_path + "\\record.txt");
        Stream stream = File.Open(directory_path + "\\record.txt", FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter filewriter = new StreamWriter(stream);

        for (int i = 0; i < powerStatList.Count; i++)
        {
            filewriter.WriteLine(powerStatList[i].getTimestamp() + "," + powerStatList[i].getVoltage() + "," + powerStatList[i].getCurrent());
        }
        filewriter.Flush();
        filewriter.Close();
    }
}
