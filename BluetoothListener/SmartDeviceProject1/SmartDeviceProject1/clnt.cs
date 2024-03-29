﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
public class clnt
{
    //static String serverIP = "manoop.dyndns.org";
    //static String claimerIP = "";
    static String serverIP = "192.168.1.6";
    //static String serverIP = "128.235.67.108";
    public static void sendToLCA(TextBox textBox1, String claimerIP, String trID, ComboBox id)
    {
        try
        {
            TcpClient tcpclnt = new TcpClient();
            //textBox1.Text = textBox1.Text + " verification msg sending to LCA!! \r\n" ;
            //Console.WriteLine("Connecting.....");
            //tcpclnt.Connect(IPAddress.Parse(serverIP), 8000); // use the ipaddress as in the server program
            tcpclnt.Connect(serverIP, 8000); // use the hostname of the server program
            //Console.WriteLine("Connected");
            //Console.Write("Enter the string to be transmitted : ");
            //                String str = Console.ReadLine();
            //String str = claimerIP.Trim() + " ClientIP - Claimer Location: Network Lab!! \n";
            String str = "verification," + trID + ","+id.Text.ToString().Trim()+",12,999,0,"+claimerIP.Trim()+" \n";
            //textBox1.Text = textBox1.Text + str;
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            //Console.WriteLine("Transmitting.....");
            int dat1 = System.Environment.TickCount;
            stm.Write(ba, 0, ba.Length);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            //textBox1.Text = textBox1.Text + "RTT for LCA: " + a.ToString() + "\r\n";
            //byte[] bb = new byte[1000];
            //int k = stm.Read(bb, 0, 1000);
            //for (int i = 0; i < k; i++)
                //textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
            tcpclnt.Close();
            //clean claimerIP - as it is a static variable
            //claimerIP = "";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error..... " + ex.StackTrace);
        }
    }

    public static void read(TextBox textBox1, ComboBox id)
    {
        //serverIP = ServerIPAddress.Text.ToString().Trim();
        //Guid gd = Guid.NewGuid();
        //MessageBox.Show("Guid: '" + gd + "'");
        //String st = System.Net.Dns.GetHostName();
        //Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
        Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
        BluetoothListener BL = new BluetoothListener(MyServiceUuid);
        
        while (true)
        {
            try
            {

                BL.Start();
                //MessageBox.Show("Accept Client.");
                textBox1.Text = "Listening for client.. \r\n";
                BluetoothClient BC = BL.AcceptBluetoothClient();
                //TextBox textBox1;
                //if (BC.RemoteMachineName.Equals(""))
                //  continue;
                //textBox1.Text = textBox1.Text + "'" + BC.RemoteMachineName + "' Get stream... \r\n";
                Stream peer = BC.GetStream();
                //MessageBox.Show("Get reader.");
                String claimerIP = "";
                String trID = "";
                byte[] bb = new byte[10000];
                int k = peer.Read(bb, 0, 10000);
                for (int i = 0; i < k; i++)
                {
                    /*if (i < 15)
                    {
                        claimerIP = claimerIP + Convert.ToChar(bb[i]);
                        //textBox1.Text = textBox1.Text + "IP: "+Convert.ToChar(bb[i]);
                    }*/
                    if (i > k - 4)
                        trID = trID + Convert.ToChar(bb[i]);
                    else
                        break;
                    //textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
                }
                //textBox1.Text = textBox1.Text + "Processing Tr_ID:"+trID;
                
                String str1 = "BT message recieved.";
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str1);
                //Console.WriteLine("Transmitting.....");
                peer.Write(ba, 0, ba.Length);

                //StreamReader rdr = new StreamReader(peer, Encoding.ASCII);
                //byte[] buf = new byte[1024];
                //int bytesRead = peer.Read(buf, 0, buf.Length);
                //MessageBox.Show("Get string.");
                //string s = Encoding.Unicode.GetString(buf, 0, bytesRead);
               // textBox1.Text = textBox1.Text + "Read line... \r\n";
                //string s = rdr.ReadLine();
                //if (s == null) //eof
                  //  textBox1.Text = textBox1.Text + "no message... \r\n";
                //else
                  //  textBox1.Text = textBox1.Text + " Message recieved: \r\n" + " " + s + "\r\n";
                sendToLCA(textBox1, claimerIP.Trim(), trID.Trim(),id);
                //BL.Stop();
                peer.Close();
            }
            catch (Exception ex)
            {
                //textBox1.Text = textBox1.Text + "Error..... " + ex.StackTrace;
                Console.WriteLine("Error..... " + ex.StackTrace);
            }
        }
    }

    public static String GetMyIP()
    {
        IPHostEntry hostentry = Dns.GetHostEntry(Dns.GetHostName());
        if (hostentry != null)
        {
            IPAddress[] collectionOfIPs = hostentry.AddressList;
            //for (int i = 0; i < collectionOfIPs.Length; i++)
            //  textBox1.Text = textBox1.Text + " IP"+i+": "+collectionOfIPs[i].ToString();
            return collectionOfIPs[0].ToString();
        }
        return "";
    }
}
