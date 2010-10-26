using System;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.IO.Ports;
using System.Collections.Generic;

public class clnt
{
    static String serverIP = "192.168.1.10";
    //static String serverIP = "192.168.1.11";
    //static String serverIP = "128.235.67.108";
    public static void sendToLCA(TextBox textBox1)
    {
        try
        {
            TcpClient tcpclnt = new TcpClient();
            //Console.WriteLine("Connecting.....");
            tcpclnt.Connect(IPAddress.Parse(serverIP), 8000); // use the ipaddress as in the server program
            //Console.WriteLine("Connected");
            //Console.Write("Enter the string to be transmitted : ");
            //                String str = Console.ReadLine();
            String str = "My Location: Network Lab!! \n";
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            //Console.WriteLine("Transmitting.....");
            stm.Write(ba, 0, ba.Length);
            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);
            for (int i = 0; i < k; i++)
                textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
            tcpclnt.Close();
            stm.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error..... " + ex.StackTrace);
        }
    }

    public static void sendToLBS(TextBox textBox1, TextBox ServerIPAddress)
    {
        serverIP = ServerIPAddress.Text.ToString().Trim();
        try
        {
            //textBox1.Text = textBox1.Text + serverIP;
            TcpClient tcpclnt = new TcpClient();
            //Console.WriteLine("Connecting.....");
            //textBox1.Text = textBox1.Text + " Connecting..... \r\n";
            tcpclnt.Connect(IPAddress.Parse(serverIP), 9000); // use the ipaddress as in the server program
            //Console.WriteLine("Connected");
            //textBox1.Text = textBox1.Text + " Connected..... \r\n";
            //Console.Write("Enter the string to be transmitted : ");
            //                String str = Console.ReadLine();
            String str = "claim: My Location = 'Network Lab' \n";
            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            //Console.WriteLine("Transmitting.....");
            //textBox1.Text = textBox1.Text + " Transmitting..... \r\n";
            stm.Write(ba, 0, ba.Length);
            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);
         
            for (int i = 0; i < k; i++)
                textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
            //textBox1.Text = textBox1.Text + " \r\n";
            tcpclnt.Close();
            stm.Close();
            //recieve(textBox1);
            textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n";
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error..... " + ex.StackTrace);
        }
    }

    public static void connect(TextBox textBox1)
    {
        try
        {
            BluetoothClient BC = new BluetoothClient();
            BluetoothAddress ADDRESS;//= new BluetoothAddress();
            
            DateTime date1 = DateTime.Now;
            BluetoothDeviceInfo[] arr = BC.DiscoverDevices();
            DateTime date2 = DateTime.Now;
            TimeSpan rtt1 = date2.Subtract(date1);
            textBox1.Text = textBox1.Text + "\r\n BT Discover Time: " + rtt1.ToString() + " \r\n ";

            Console.WriteLine("There were " + arr.Length + " devices found:");

            foreach (BluetoothDeviceInfo b in arr)
            {
                Console.WriteLine("");
                Console.WriteLine(("").PadRight(24, '-'));
                Console.WriteLine("\t" + b.DeviceName);
                Console.WriteLine("\t" + b.ClassOfDevice);
                Console.WriteLine("\t" + b.Connected);
                Console.WriteLine("\t" + b.DeviceAddress);

                Console.WriteLine("\t" + b.InstalledServices);
                Console.WriteLine("\t" + b.LastSeen);
                Console.WriteLine("\t" + b.LastUsed);
                Console.WriteLine("\t" + b.Remembered);
                Console.WriteLine(("").PadRight(24, '-'));

                ADDRESS = b.DeviceAddress;
                Console.WriteLine("\t" + ADDRESS.Sap);
                Console.WriteLine("\t" + ADDRESS.Nap);

                textBox1.Text = textBox1.Text +"Found Bluetooth DeviceName: '" + b.DeviceName + "'\r\n";
                String st = b.DeviceName;
                st.Trim();
                if (st.Contains("Pocket_PC"))
                {
                    try
                    {
                        
                        BC = new BluetoothClient();
                        
                        //BluetoothEndPoint ep = new BluetoothEndPoint(ADDRESS, BluetoothService.SerialPort);
                        //System.Guid.NewGuid();
                        Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
                        BluetoothEndPoint ep = new BluetoothEndPoint(ADDRESS, MyServiceUuid);
                        //textBox1.Text = textBox1.Text + "Conneting...\r\n";
                        BC.Connect(ep);
                        textBox1.Text = textBox1.Text + "Connected: \t " + BC.Connected + "\r\n";

                        String localIP = GetMyIP();

                        Byte[] msg;
                        msg = System.Text.Encoding.UTF8.GetBytes(localIP + "     ClientIP - My Location: Network Lab!!");

                        Stream peerStream = BC.GetStream();
                        //string cmd = "$PASHS,RID";
                        textBox1.Text = textBox1.Text + "Sending verification request to " + b.DeviceName + " \r\n";
                        /*int temp = 0;
                        String lng= "x";
                        while (temp != 20)
                        {
                            lng = lng + " Test: " + temp + " My Location: Network Lab!! ";
                            
                            temp++;
                        }
                        msg = System.Text.Encoding.UTF8.GetBytes(lng);
                        */
                        //msg = System.Text.Encoding.UTF8.GetBytes("My Location: Network Lab!!");
                        DateTime date3 = DateTime.Now;
                        
                        peerStream.Write(msg, 0, msg.Length);

                        byte[] bb1 = new byte[10000];
                        int k = peerStream.Read(bb1, 0, 10000);
                        DateTime date4 = DateTime.Now;
                        TimeSpan rtt2 = date4.Subtract(date3);
                        textBox1.Text = textBox1.Text+"\r\n time taken to send certification request: "+rtt2.ToString()+"\r\n";
                        for (int i = 0; i < k; i++)
                        {
                            textBox1.Text = textBox1.Text + Convert.ToChar(bb1[i]);
                        }

                        peerStream.Close();
                        BC.Client.Close();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Error..... " + ex.StackTrace);
                        textBox1.Text = textBox1.Text + "Error..... " + ex.StackTrace;
                    }
                    
                }


                Console.ReadLine();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error..... " + ex.StackTrace);
        }
    }

    public static void recieve(TextBox textBox1)
    {
        try
        {
            //
            //String localIP = Dns.GetHostEntry(Dns.GetHostName()).ToString();
            String localIP = GetMyIP();
            //String localIP = ((serverSocket.getInetAddress()).getLocalHost()).getHostAddress();
            textBox1.Text = textBox1.Text + " localIP: "+localIP;
            IPAddress ipAd = IPAddress.Parse(localIP); //use local m/c IP address, and use the same in the client
            /* Initializes the Listener */
            TcpListener myList = new TcpListener(ipAd, 8000);
            /* Start Listeneting at the specified port */
            myList.Start();
            //Console.WriteLine("The server is running at port 8001...");
            //Console.WriteLine("The local End point is :" + myList.LocalEndpoint);
            //Console.WriteLine("Waiting for a connection.....");
            Socket s = myList.AcceptSocket();
            textBox1.Text = textBox1.Text + "Message from LBS: " + s.RemoteEndPoint + " \r\n";
            byte[] b = new byte[100];
            int k = s.Receive(b);
            //textBox1.Text = textBox1.Text + "Recieved... \r\n";
            for (int i = 0; i < k; i++)
                textBox1.Text = textBox1.Text + Convert.ToChar(b[i]);
            textBox1.Text = textBox1.Text + " \r\n";
            ASCIIEncoding asen = new ASCIIEncoding();
            //s.Send(asen.GetBytes("The string was recieved by the server."));
            //Console.WriteLine("\nSent Acknowledgement");
            /* clean up */
            s.Close();
            myList.Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
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

    public static void testRSA(TextBox textBox1)
    {
        try
        {
            // Create a UnicodeEncoder to convert between byte array and string.
            ASCIIEncoding ByteConverter = new ASCIIEncoding();

            string dataString = "Data to Sign";
            textBox1.Text = textBox1.Text + "Input String: "+dataString+" \r\n";
            // Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] signedData;

            // Create a new instance of the RSACryptoServiceProvider class 
            // and automatically create a new key-pair.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            
            // Export the key information to an RSAParameters object.
            // You must pass true to export the private key for signing.
            // However, you do not need to export the private key
            // for verification.
            RSAParameters Key = RSAalg.ExportParameters(true);

            // Hash and sign the data.
            int dat1 = System.Environment.TickCount;
            signedData = HashAndSignBytes(originalData, Key);
            int dat2 = System.Environment.TickCount;
            StringBuilder a = new StringBuilder();
            a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
            textBox1.Text = textBox1.Text + "Time taken for signing: " + a.ToString() + "\r\n Signed data: " + " \r\n";
            /*
            for (int i = 0; i < signedData.Length;i++ )
            {
                textBox1.Text = textBox1.Text + signedData[i];
            }
            */
            textBox1.Text = textBox1.Text + " \r\n";
            // Verify the data and display the result to the 
            // console.
            int dat3 = System.Environment.TickCount;
            if (VerifySignedHash(originalData, signedData, Key))
            {
                int dat4 = System.Environment.TickCount;
                StringBuilder b = new StringBuilder();
                b.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat4 - dat3));

                textBox1.Text = textBox1.Text + "The data was verified in time: " + b.ToString() + " \r\n";
            }
            else
            {
                textBox1.Text = textBox1.Text + "The data does not match the signature." + " \r\n";
            }

        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("The data was not signed or verified");

        }
    }

    public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
    {
        try
        {
            // Create a new instance of RSACryptoServiceProvider using the 
            // key from RSAParameters.  
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            
            RSAalg.ImportParameters(Key);

            // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider
            // to specify the use of SHA1 for hashing.
            return RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }
    }

    public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
    {
        try
        {
            // Create a new instance of RSACryptoServiceProvider using the 
            // key from RSAParameters.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            RSAalg.ImportParameters(Key);
            
            // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider
            // to specify the use of SHA1 for hashing.
            return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);

        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return false;
        }
    }
}
