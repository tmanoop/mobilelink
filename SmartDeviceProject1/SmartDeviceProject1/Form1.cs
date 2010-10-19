using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;


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
            clnt.sendToLCA(textBox1);
            clnt.connect(textBox1);
            
            
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Sending Location claim to LBS.. \r\n";
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            TimeSpan rtt1 = date2.Subtract(date1);
            roundTripTime.Text = rtt1.ToString();
            clnt.sendToLBS(textBox1, ServerIPAddress);
            DateTime date3 = DateTime.Now;
            TimeSpan rtt = date3.Subtract(date1);
            roundTripTime.Text = rtt.ToString();
            //clnt.recieve(textBox1);
            // Forces the Garbage Collect to run on the system.
            System.GC.Collect();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            clnt.testRSA(textBox1);
        }

    }
}