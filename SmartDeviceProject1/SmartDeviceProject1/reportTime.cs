using System;
using System.Collections.Generic;
using System.IO;

namespace SmartDeviceProject1
{
    public class report
    {
        public report()
        {
        }
        public static void writeToFile(List<String> timeStatList)
        {
            writeToFile(timeStatList, "time.txt", true);
        }

        public static void writeToFile(List<String> timeStatList,String filename)
        {
            writeToFile(timeStatList, filename, true);
        }

        public static void writeToFile(List<String> timeStatList,String filename, Boolean deleteOld)
        {
            String full_path = System.Reflection.Assembly.GetCallingAssembly().GetName().CodeBase;
            String directory_path = full_path.Substring(0, full_path.LastIndexOf("\\"));
            Stream stream = null;
            StreamWriter filewriter = null;
            if (deleteOld == true)
            {
                File.Delete(directory_path + "\\" + filename);
                stream = File.Open(directory_path + "\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
                filewriter = new StreamWriter(stream);
            }
            else
            {
                
                filewriter = File.AppendText(directory_path + "\\" + filename);
            }

            for (int i = 0; i < timeStatList.Count; i++)
            {
                filewriter.WriteLine(timeStatList[i].ToString());
            }
            filewriter.Flush();
            filewriter.Close();
        }
    }
}