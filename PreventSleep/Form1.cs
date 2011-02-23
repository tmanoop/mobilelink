using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32;
using Microsoft.Win32;

namespace PreventSleep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Look in the registry to see what the shortest timeout
        // period is. Note that Zero is a special value with respect
        // to timeouts. It indicates that a timeout will not occur.
        // As long as as SystemIdleTimeerReset is called on intervals
        // that are shorter than the smallest non-zero timeout value
        // then the device will not sleep from idleness. This does
        // not prevent the device from sleeping due to the power 
        // button being pressed. 
        int ShortestTimeoutInterval()
        {
            int retVal = 1000;
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"\SYSTEM\CurrentControlSet\Control\Power");
            object oBatteryTimeout = key.GetValue("BattPowerOff");
            object oACTimeOut = key.GetValue("ExtPowerOff");
            object oScreenPowerOff = key.GetValue("ScreenPowerOff");

            if (oBatteryTimeout is int)
            {
                int v =  (int)oBatteryTimeout;
                if(v>0)
                    retVal = Math.Min(retVal,v);
                txtBatteryIdleTimeOut.Text = oBatteryTimeout.ToString();
            }
            if (oACTimeOut is int)
            {
                int v = (int)oACTimeOut;
                if(v>0)
                    retVal = Math.Min(retVal, v);
                txtACTimeout.Text = oACTimeOut.ToString();
            }
            if (oScreenPowerOff is int)
            {
                int v = (int)oScreenPowerOff;
                if(v>0)
                    retVal = Math.Min(retVal, v);
                txtScreenOff.Text = oScreenPowerOff.ToString();
            }

            return retVal*9/10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the interval on our timer and start the
            // timer.  It will run for the duration of the
            // program
            int interval = ShortestTimeoutInterval();
            resetTimer.Interval = interval;
            resetTimer.Enabled = true;
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Call the SystemIdleTimerReset method to prevent the 
        // device from sleeping
        private void resetTimer_Tick(object sender, EventArgs e)
        {
            CoreDLL.SystemIdleTimerReset();
        }
    }
}