using System;

using System.Collections.Generic;
using System.Windows.Forms;
using Win32;
using System.IO;

namespace WorkQuietly
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
                Application.Run(new Form1());
            else if (args[0].Equals("AppRunAtTime"))
            {
                string soundPath;

                // We started due to a scheduled event
                CoreDLL.PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, -1);
                string targetExecutable = typeof(Form1).Assembly.GetModules()[0].FullyQualifiedName;

                StreamWriter argInfo = new StreamWriter(targetExecutable + ".argument.txt");
                argInfo.WriteLine(args[0]);
                argInfo.Close();

                using (StreamReader sr = new StreamReader(targetExecutable + ".soundPath"))
                {
                    soundPath = sr.ReadToEnd();
                    sr.Close();
                }
                if (File.Exists(soundPath))
                    Aygshell.SndPlaySync(soundPath, 0);
                CoreDLL.PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, 0);
            }
        }
    }
}