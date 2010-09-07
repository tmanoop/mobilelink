#region using directives
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Collections;
#endregion

namespace btcomm1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public LinkLabel linklabel1;
        public LinkLabel linklabel2;
        public Guid service = BluetoothService.DialupNetworking;
        public BluetoothClient bluetoothClient;

        private void Form1_Load(object sender, EventArgs e) 
        {

        }

  

          
       private void linkLabel1_Click(object sender, EventArgs e)
        {
            //BluetoothRadio.PrimaryRadio.Mode = RadioMode.Discoverable();
            bluetoothClient = new BluetoothClient();
            Cursor.Current = Cursors.WaitCursor;
            BluetoothDeviceInfo[] bluetoothDeviceInfo = { };
            bluetoothDeviceInfo = bluetoothClient.DiscoverDevices(10);
            comboBox1.DataSource = bluetoothDeviceInfo;
            comboBox1.DisplayMember = "DeviceName";
            comboBox1.ValueMember = "DeviceAddress";
            comboBox1.Focus();
            Cursor.Current = Cursors.Default; 
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                try
                {
                    bluetoothClient.Connect(new BluetoothEndPoint((BluetoothAddress)comboBox1.SelectedValue, service));
                    MessageBox.Show("Connected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}