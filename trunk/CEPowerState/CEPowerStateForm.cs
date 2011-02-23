using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32;
using Microsoft.Win32;

namespace CEPowerState
{
    public partial class CEPowerStateForm : Form
    {
        

        public CEPowerStateForm()
        {
            InitializeComponent();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miSetState_Click(object sender, EventArgs e)
        {
            //Get the name of the selected hardware
            string deviceName = lstDeviceList.SelectedItem as string;
            //Get the power state to which the device will be changed
            CEDEVICE_POWER_STATE state = (CEDEVICE_POWER_STATE)Enum.Parse(typeof(CEDEVICE_POWER_STATE), lstPowerState.SelectedItem as string,true);
            
            //deviceHandle = CoreDLL.SetPowerRequirement(deviceName, state, (DevicePowerFlags)1 , IntPtr.Zero, 0);            
           CoreDLL.SetDevicePower(deviceName, DevicePowerFlags.POWER_NAME, state);
                        
        }

        private void CEPowerStateForm_Load(object sender, EventArgs e)
        {
            // Get the names of all of the subkeys that
            // refer to hardware on the device. 
           RegistryKey driverKeyRoot =  Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Drivers\\Active");
           string[] keyName = driverKeyRoot.GetSubKeyNames();

            //We are saving this information to list for sorting later
           List<string> deviceNameList = new List<string>();
           for (int i = 0; i < keyName.Length; ++i)
           {
               //Get the name of the hardware and add it to the list
               RegistryKey currentKey = driverKeyRoot.OpenSubKey(keyName[i]);
               string deviceName = currentKey.GetValue("Name") as string;
               if(deviceName!=null)
                deviceNameList.Add(deviceName);
               
           }
            //Sort the list
           deviceNameList.Sort();
            //Add the list to the list box so the user can select hardware
           for (int i = 0; i < deviceNameList.Count; ++i)
           {
               lstDeviceList.Items.Add(deviceNameList[i]);
           }


        
        }
    }
}