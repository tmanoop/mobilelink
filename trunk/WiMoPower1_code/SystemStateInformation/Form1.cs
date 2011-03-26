using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile.Status;

namespace SystemStateInformation
{
    public partial class Form1 : Form
    {

        SystemState _systemState = new SystemState(SystemProperty.PowerBatteryBackupStrength);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _systemState.Changed += new ChangeEventHandler(systemState_Changed);

            lvBatteryState.Items.Add(new ListViewItem(new string[] { "Phone Radio Off", "" }));
            lvBatteryState.Items.Add(new ListViewItem(new string[] { "Backup Battery State", "" }));
            lvBatteryState.Items.Add(new ListViewItem(new string[] { "Backup Battery Strength", "" }));
            lvBatteryState.Items.Add(new ListViewItem(new string[] { "Primary Battery State", "" }));
            lvBatteryState.Items.Add(new ListViewItem(new string[] { "Primary Battery Strength", "" }));
            UpdateBatteryStats();
        }

        void systemState_Changed(object sender, ChangeEventArgs args)
        {
            UpdateBatteryStats();
        }

        void UpdateBatteryStats()
        {
            UpdateListItem(0, SystemState.PhoneRadioOff.ToString());
            UpdateListItem(1, SystemState.PowerBatteryBackupState.ToString());
            UpdateListItem(2, SystemState.PowerBatteryBackupStrength.ToString());
            UpdateListItem(3, SystemState.PowerBatteryState.ToString());
            UpdateListItem(4, SystemState.PowerBatteryStrength.ToString());            

        }

        void UpdateListItem(int index, string val)
        {
            lvBatteryState.Items[index].SubItems[1].Text = val;
        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            UpdateBatteryStats();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}