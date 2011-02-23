using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32;

namespace WorkQuietly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Credit for this method goes to Jim Wilson of JW HedgeHog
        public static void RunAppAtTime(string applicationEvent, DateTime startTime)
        {
            long fileTimeUTC = startTime.ToFileTime();
            long fileTimeLocal = 0 ;
            SystemTime systemStartTime = new SystemTime();
            CoreDLL.FileTimeToLocalFileTime(ref fileTimeUTC, ref fileTimeLocal);
            CoreDLL.FileTimeToSystemTime(ref fileTimeLocal, systemStartTime);
            CoreDLL.CeRunAppAtTime(applicationEvent, systemStartTime);
        }
        public static void RunAppAtTime(
             string applicationEvent, 
             TimeSpan timeDisplacement
        )
        {
            DateTime targetTime = DateTime.Now + timeDisplacement;
            RunAppAtTime(applicationEvent, targetTime);
        }

        // Have the user select a sound file to be played
        private void miOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sound File |*.wav;*.mp3;*.wma";
            ofd.FileName = txtSoundPath.Text;
            if (DialogResult.OK == ofd.ShowDialog())
            {
                txtSoundPath.Text = ofd.FileName;
            }
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Schedule the execution of the program and terminate. 
        // You can either pur the device to sleep or leave it at
        // full power.  In either case the program will run at its
        // assigned time, play a sound, and then terminate
        private void miRun_Click(object sender, EventArgs e)
        {                        
           int waitTime = int.Parse(this.cboStartTime.Text);
            DateTime startTime = DateTime.Now.AddSeconds(waitTime);
            string targetExecutable = this.GetType().Assembly.GetModules()[0].FullyQualifiedName;
            RunAppAtTime(targetExecutable, startTime);
            using (StreamWriter sw = new StreamWriter(targetExecutable + ".soundPath"))
            {
                sw.Write(txtSoundPath.Text);
                sw.Close();
            }
            this.Close();
        }
    }
}