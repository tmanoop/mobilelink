using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Diagnostics;


namespace SmartDeviceProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //Application.DoEvents();
            //OnPaint(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Create string to draw.
            string drawString = "LINK - Coupon Service";

            // Create font and brush.
            Font drawFont = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            float x = 10.0F;
            float y = 10.0F;

            // Draw string to screen.
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuItem3_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Sending Location claim to LCA.. \r\n";
            int dat1 = System.Environment.TickCount;
            clnt.sendToLCA(textBox1,loc,id,0);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            textBox1.Text = textBox1.Text + "RTT for TCP: "+a.ToString()+"\r\n";

            clnt.connect(textBox1, loc, id);
            
            
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            //textBox1.Text = "Sending Location claim to LBS.. \r\n";
            int dat1 = System.Environment.TickCount;
            clnt.sendToLBS(textBox1);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
            //roundTripTime.Text = a.ToString();
            String rttTCP = a.ToString();


            //textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n";
            //clnt.sendToLCA(textBox1,loc,id);
            //bluetooth call moved back to lca function to pass trID
            String rttBLTH = clnt.connect(textBox1, loc, id);
            String lcaResponse = clnt.recieve(textBox1);
            int dat3 = System.Environment.TickCount;
            StringBuilder b = new StringBuilder();
            b.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat3 - dat1));
            roundTripTime.Text = b.ToString();
            textBox1.Text = "LCA Response: " + lcaResponse + "\r\n";
            textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
            textBox1.Text = textBox1.Text + "BluetoothDiscovery: " + rttBLTH + "\r\n";
            
            // Forces the Garbage Collect to run on the system.
            System.GC.Collect();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            clnt.testRSA(textBox1);
            //SecurityManager.Test();
        }

        private void menuItem2_Click_2(object sender, EventArgs e)
        {
            //Test WiFi RTT
            DateTime startTime = DateTime.Now;
            DateTime currTime = DateTime.Now;;
            TimeSpan elapsedTime = currTime - startTime;

            // Create a new List
            List<String> timeStatList = new List<String>();
            //run test for 1 hour
            //int x = 0;
            while (elapsedTime.TotalMinutes <= 60)
            //while (x <= 0)
            {
                int dat1 = System.Environment.TickCount;
                clnt.sendToLBS(textBox1);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                //x = 1;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            report.writeToFile(timeStatList);
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            //Test BT Disc
            DateTime startTime = DateTime.Now;
            DateTime currTime = DateTime.Now;

            TimeSpan elapsedTime = currTime - startTime;

            // Create a new List
            List<String> timeStatList = new List<String>();
            //run test for 1 hour
            //int x = 0;
            while (elapsedTime.TotalMinutes <= 60)
            //while (x <= 0)
            {
                int dat1 = System.Environment.TickCount;
                clnt.bluetoothDiscovery();
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                //x = 1;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            report.writeToFile(timeStatList);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            //Test BT RTT
            DateTime startTime = DateTime.Now;
            DateTime currTime = DateTime.Now;

            TimeSpan elapsedTime = currTime - startTime;

            // Create a new List
            List<String> timeStatList = new List<String>();
            //run test for 1 hour
            //int x = 0;
            BluetoothDeviceInfo[] arr1 = clnt.bluetoothDiscovery();
            textBox1.Text = textBox1.Text + "Number of BT devices: " + arr1.Length + " \r\n";
            textBox1.Text = textBox1.Text + "BT device: " + arr1[0].DeviceName + " \r\n";
            Byte[] msg = System.Text.Encoding.UTF8.GetBytes("Test");
            while (elapsedTime.TotalMinutes <= 60 && arr1.Length != 0)
            //while (x <= 0 && arr1.Length != 0)
            {
                int dat1 = System.Environment.TickCount;
                clnt.bluetoothTestConnection(textBox1,arr1[0].DeviceAddress,msg);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                //x++;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            report.writeToFile(timeStatList);
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            //Test signing
            DateTime startTime = DateTime.Now;
            DateTime currTime = DateTime.Now;

            TimeSpan elapsedTime = currTime - startTime;

            // Create a new List
            List<String> timeStatList = new List<String>();
            //run test for 1 hour
            //int x = 0;
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAParameters Key = RSAalg.ExportParameters(true);
            string dataString = "claim: 01,12,999,0";
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] signedData;
            while (elapsedTime.TotalMinutes <= 60)
            //while (x <= 0)
            {
                int dat1 = System.Environment.TickCount;
                signedData = clnt.HashAndSignBytes(originalData, Key);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + signedData.ToString() + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                //x = 1;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            report.writeToFile(timeStatList);
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            //Test verifying
            DateTime startTime = DateTime.Now;
            DateTime currTime = DateTime.Now;

            TimeSpan elapsedTime = currTime - startTime;

            // Create a new List
            List<String> timeStatList = new List<String>();
            //run test for 1 hour
            //int x = 0;
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAParameters Key = RSAalg.ExportParameters(true);
            string dataString = "claim: 01,12,999,0";
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] signedData = clnt.HashAndSignBytes(originalData, Key);
            while (elapsedTime.TotalMinutes <= 60)
            //while (x <= 0)
            {
                int dat1 = System.Environment.TickCount;
                bool verfied = clnt.VerifySignedHash(originalData, signedData, Key);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "verfied: " + verfied + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                //x = 1;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            report.writeToFile(timeStatList);
        }
    }
}