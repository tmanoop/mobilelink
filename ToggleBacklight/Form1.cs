using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32;

namespace ToggleBacklight
{
    public partial class Form1 : Form
    {
        IntPtr hBacklight = IntPtr.Zero;
        public Form1()
        {
            InitializeComponent();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdToggleBacklight_Click(object sender, EventArgs e)
        {
            
            if (hBacklight == IntPtr.Zero)
                hBacklight = CoreDLL.SetPowerRequirement("BKL1:",CEDEVICE_POWER_STATE.D4,DevicePowerFlags.POWER_NAME,IntPtr.Zero,0);                
            else
            {
                CoreDLL.ReleasePowerRequirement(hBacklight);
                hBacklight = IntPtr.Zero;
            }

           
        }
    }
}