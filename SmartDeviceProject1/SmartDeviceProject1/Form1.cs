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
            BluetoothDeviceInfo[] b = new BluetoothDeviceInfo[2];
            int dat1 = System.Environment.TickCount;
            clnt.sendToLCA(textBox1,loc,id,b);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            textBox1.Text = textBox1.Text + "RTT for TCP: "+a.ToString()+"\r\n";

            clnt.connect(textBox1, loc, id);
            
            
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            //below Test2 is asynch and did not work when running on WIN MOBILE. It worked only in debug mode deployment to phone.
            //linkProtocol2();
            //For test1, uncomment below section
            // Create a new List
            List<String> timeStatList = new List<String>();

            String[] linkResponseTimes = linkProtocol1();

            String test = "";
            for (int i = 1; i < linkResponseTimes.Length; i++)
            {
                test = test + linkResponseTimes[i] + ",";
            }
            timeStatList.Add(test);
            report.writeToFile(timeStatList, "LinkRTT.txt", false);

            textBox1.Text = textBox1.Text + "LBS Response: " + linkResponseTimes[0] + "\r\n";
            textBox1.Text = textBox1.Text + "LBS RTT: " + linkResponseTimes[1] + "\r\n";
            textBox1.Text = textBox1.Text + "BT DISC: " + linkResponseTimes[2] + "\r\n";
            textBox1.Text = textBox1.Text + "LCA RTT: " + linkResponseTimes[3] + "\r\n";
            textBox1.Text = textBox1.Text + "BT 1 Conn: " + linkResponseTimes[4] + "\r\n";
            textBox1.Text = textBox1.Text + "BT ALL Conn: " + linkResponseTimes[5] + "\r\n";
            textBox1.Text = textBox1.Text + "Signing: " + linkResponseTimes[6] + "\r\n";
            textBox1.Text = textBox1.Text + "verifying: " + linkResponseTimes[7] + "\r\n";
            textBox1.Text = textBox1.Text + "filter: " + linkResponseTimes[8] + "\r\n";
            textBox1.Text = textBox1.Text + "LBS Response time: " + linkResponseTimes[9] + "\r\n";
            roundTripTime.Text = linkResponseTimes[10];
            textBox1.Text = textBox1.Text + "connect time: " + linkResponseTimes[11] + "\r\n";
        }

        private void linkProtocol2()
        {
            BluetoothCall bCall = new BluetoothCall(textBox1, loc, id);
            bCall.connect(textBox1, loc, id);
        }
        private String[] linkProtocol1()
        {
            //textBox1.Text = "Sending Location claim to LBS.. \r\n";
            int dat1 = System.Environment.TickCount;
            clnt.sendToLBS(textBox1);
            int dat2 = System.Environment.TickCount;
            int a = dat2 - dat1;
            //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
            //roundTripTime.Text = a.ToString();
            String lbsTime = a.ToString();


            //textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n";
            //clnt.sendToLCA(textBox1,loc,id);
            //bluetooth call moved back to lca function to pass trID
            String[] delayTimes = new String[7];
            int connectTime1 = System.Environment.TickCount;
            delayTimes = clnt.connect(textBox1, loc, id);
            //BluetoothCall bCall = new BluetoothCall(textBox1, loc, id);
            //delayTimes = bCall.connect(textBox1, loc, id);
            int lbs = System.Environment.TickCount;
            String lbsResponse = clnt.recieve(textBox1);
            int dat3 = System.Environment.TickCount;
            int lbsFinTime = dat3 - lbs;

            int connectTime = lbs - connectTime1;

            int linkRTT = dat3 - dat1;
            
            String[] linkResponseTimes = new String[12];
            linkResponseTimes[0] = lbsResponse;
            linkResponseTimes[1] = lbsTime;
            linkResponseTimes[2] = delayTimes[0];
            linkResponseTimes[3] = delayTimes[1];
            linkResponseTimes[4] = delayTimes[2];
            linkResponseTimes[5] = delayTimes[3];
            linkResponseTimes[6] = delayTimes[4];
            linkResponseTimes[7] = delayTimes[5];
            linkResponseTimes[8] = delayTimes[6];
            linkResponseTimes[9] = lbsFinTime.ToString();
            linkResponseTimes[10] = linkRTT.ToString();
            linkResponseTimes[11] = connectTime.ToString();

            // Forces the Garbage Collect to run on the system.
            System.GC.Collect();

            return linkResponseTimes;
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
            int x = 0;
            //while (elapsedTime.TotalMinutes <= 60)
            while (x <= 0)
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
                x++;
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
            int x = 0;
            //while (elapsedTime.TotalMinutes <= 60)
            while (x <= 0)
            {
                int dat1 = System.Environment.TickCount;
                BluetoothDeviceInfo[] arr1 = clnt.bluetoothDiscovery();
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
                //roundTripTime.Text = a.ToString();
                String rttTCP = "";
                foreach (BluetoothDeviceInfo b in arr1)
                {
                    if (b != null)
                    {
                        textBox1.Text = textBox1.Text + b.DeviceAddress.ToString() + " \r\n";
                        rttTCP = rttTCP + b.DeviceAddress.ToString() + " \r\n";
                    }
                }

                //String rttTCP = a.ToString();
                timeStatList.Add(rttTCP);
                //textBox1.Text = textBox1.Text + startTime.Second + " \r\n";
                currTime = DateTime.Now;
                elapsedTime = currTime - startTime;
                //textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
                x++;
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
            int x = 0;
            BluetoothDeviceInfo[] arr1 = clnt.bluetoothDiscovery();
            textBox1.Text = textBox1.Text + "Number of BT devices: " + arr1.Length + " \r\n";
            textBox1.Text = textBox1.Text + "BT device: " + arr1[0].DeviceName + " \r\n";
            Byte[] msg = System.Text.Encoding.UTF8.GetBytes("Test");
            //while (elapsedTime.TotalMinutes <= 60 && arr1.Length != 0)
            while (x <= 0 && arr1.Length != 0)
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
                x++;
            }
            textBox1.Text = textBox1.Text + "elapsedTime: " + elapsedTime.TotalMinutes + " \r\n";
            textBox1.Text = textBox1.Text + "BT RTT: " + timeStatList[0] + " \r\n";
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

        private void menuItem9_Click(object sender, EventArgs e)
        {
            multiTest1();
        }

        private void multiTest1()
        {
            // Create a new List
            List<String> timeStatList = new List<String>();

            int testRound = 0;
            while (testRound < 20)
            {
                String[] linkResponseTimes = linkProtocol1();

                String test = "";
                for (int i = 1; i < linkResponseTimes.Length; i++)
                {
                    test = test + linkResponseTimes[i] + ",";
                }
                timeStatList.Add(test);
                report.writeToFile(timeStatList, "LinkRTT.txt", false);
                testRound++;
            }
            report.writeToFile(timeStatList, "LinkRTT.txt");
            textBox1.Text = "Multiple tests completed.";
        }

        private void multiTest2()
        {
            int testRound = 0;
            while (testRound < 20)
            {
                linkProtocol2();
                testRound++;
            }
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "Calling BT async" + " \r\n";
            //call asynch BT DISC
            BluetoothCall bCall = new BluetoothCall(textBox1,loc,id);
            bCall.bluetoothDiscoveryAsynch();
        }
    }
}