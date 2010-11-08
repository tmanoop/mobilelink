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
            int dat1 = System.Environment.TickCount;
            clnt.sendToLCA(textBox1,loc,id);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            textBox1.Text = textBox1.Text + "RTT for TCP: "+a.ToString()+"\r\n";

            clnt.connect(textBox1,"0");
            
            
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Sending Location claim to LBS.. \r\n";
            int dat1 = System.Environment.TickCount;
            clnt.sendToLBS(textBox1, ServerIPAddress);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            textBox1.Text = textBox1.Text + "RTT for TCP: " + a.ToString() + "\r\n";
            roundTripTime.Text = a.ToString();

            textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n";
            clnt.sendToLCA(textBox1,loc,id);
            //bluetooth call moved back to lca function to pass trID
            //clnt.connect(textBox1);
            clnt.recieve(textBox1);
            int dat3 = System.Environment.TickCount;
            StringBuilder b = new StringBuilder();
            b.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat3 - dat1));
            roundTripTime.Text = b.ToString();
            // Forces the Garbage Collect to run on the system.
            System.GC.Collect();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            clnt.testRSA(textBox1);
        }

    }
}