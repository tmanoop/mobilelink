using System;
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
    public static void send()
    {
        try
        {
            TcpClient tcpclnt = new TcpClient();
            Console.WriteLine("Connecting.....");
            tcpclnt.Connect("192.168.1.8", 8000); // use the ipaddress as in the server program
            Console.WriteLine("Connected");
            Console.Write("Enter the string to be transmitted : ");
            String str = Console.ReadLine();
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            Console.WriteLine("Transmitting.....");
            stm.Write(ba, 0, ba.Length);
            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);
            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(bb[i]));
            tcpclnt.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
        }
    }

    public static void read(TextBox textBox1)
    {
        //Guid gd = Guid.NewGuid();
        //MessageBox.Show("Guid: '" + gd + "'");
        Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
        BluetoothListener BL = new BluetoothListener(MyServiceUuid);
        
        while (true)
        {
            BL.Start();
            //MessageBox.Show("Accept Client.");
            textBox1.Text = textBox1.Text + "Listening for client.. \r\n";
            BluetoothClient BC = BL.AcceptBluetoothClient();
            //TextBox textBox1;

            textBox1.Text = textBox1.Text + "'"+BC.RemoteMachineName+"' Get stream... \r\n";
            Stream peer = BC.GetStream();
            //MessageBox.Show("Get reader.");
            StreamReader rdr = new StreamReader(peer,Encoding.ASCII);
            //byte[] buf = new byte[1024];
            //int bytesRead = peer.Read(buf, 0, buf.Length);
            //MessageBox.Show("Get string.");
            //string s = Encoding.Unicode.GetString(buf, 0, bytesRead);
            string s = rdr.ReadLine();
            if (s == null) //eof
                MessageBox.Show("No message!!");
            else
                textBox1.Text = textBox1.Text + " Message recieved: \r\n" + " "+s+"\r\n";
            BL.Stop();
            peer.Close();
        }
    }
}
