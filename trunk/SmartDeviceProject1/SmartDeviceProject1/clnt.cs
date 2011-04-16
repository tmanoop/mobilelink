using System;
using System.Threading;
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

namespace SmartDeviceProject1
{
    public class clnt
    {
        static RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
        static RSAParameters Key = RSAalg.ExportParameters(true);

        static String serverIP = "manoop.dyndns.org";
        static readonly object _locker = new object();
        //static String serverIP = "192.168.1.11";
        //static String serverIP = "128.235.67.108";
        public static List<String> sendToLCA(TextBox textBox1, ComboBox loc, ComboBox id, BluetoothDeviceInfo[] arr1)
        {
            try
            {
                //textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n" + GetMyIP();
                TcpClient tcpclnt = new TcpClient();
                //Console.WriteLine("Connecting.....");
                //tcpclnt.Connect(IPAddress.Parse(serverIP), 8000); // use the ipaddress as in the server program
                tcpclnt.Connect(serverIP, 8000); // use the hostname of the server program
                //Console.WriteLine("Connected");
                //Console.Write("Enter the string to be transmitted : ");
                //                String str = Console.ReadLine();
                String vrfrAddresses = "";
                foreach(BluetoothDeviceInfo b in arr1)
                {
                    vrfrAddresses = vrfrAddresses + b.DeviceAddress + ",";
                }

                String str = "claim," + id.Text.ToString().Trim() + "," + loc.Text.ToString().Trim() + ",999,0," + GetMyIP() + "," + vrfrAddresses + " \n";
                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                //Console.WriteLine("Transmitting.....");
                int dat1 = System.Environment.TickCount;
                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                //textBox1.Text = textBox1.Text + "RTT for LCA: " + a.ToString() + "\r\n";
                List<String> responseList = new List<String>();
                int j = 0;
                String temp = "";
                while (j<k)
                {
                    temp = "" + Convert.ToChar(bb[j++]);
                    String value = "";
                    while (j<k && temp != ",")
                    {
                        value = value + temp;
                        temp = "" + Convert.ToChar(bb[j++]);
                    }
                    //textBox1.Text = textBox1.Text + "res: "+value +" \r\n";
                    responseList.Add(value);
                }
                /*
                String trID = "";
                for (int i = 0; i < k; i++)
                {
                    if (i < 3)
                    {
                        trID = trID + Convert.ToChar(bb[i]);
                        //textBox1.Text = textBox1.Text + "IP: "+Convert.ToChar(bb[i]);
                    }
                    else
                    {
                        break;
                    }
                    //textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
                }
                */
                tcpclnt.Close();
                stm.Close();
                //connect(textBox1,trID.Trim());
                return responseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error..... " + ex.StackTrace);
                return null;
            }
        }

        public static void sendToLBS(TextBox textBox1)
        {
            //int wifi1 = System.Environment.TickCount;
            //serverIP = ServerIPAddress.Text.ToString().Trim();
            try
            {
                //textBox1.Text = textBox1.Text + serverIP;
                TcpClient tcpclnt = new TcpClient();
                
                //textBox1.Text = textBox1.Text + " Connecting..... \r\n";
                //tcpclnt.Connect(IPAddress.Parse(serverIP), 9000); // use the ipaddress as in the server program
                tcpclnt.Connect(serverIP, 9000); // use the hostname of the server program
                
                //textBox1.Text = textBox1.Text + " Connected..... \r\n";
                
                String str = "claim: My current Location \n";
                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);

                //textBox1.Text = textBox1.Text + " Transmitting..... \r\n";
                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                //for (int i = 0; i < k; i++)
                //  textBox1.Text = textBox1.Text + Convert.ToChar(bb[i]);
                //textBox1.Text = textBox1.Text + " \r\n";
                tcpclnt.Close();
                stm.Close();
                //recieve(textBox1);
            }
            catch (Exception ex)
            {
                //textBox1.Text = textBox1.Text + "LBS exception: " + ex.ToString() + " \r\n";
            }
            //int wifi2 = System.Environment.TickCount;
            //int wifi = wifi2 - wifi1;
            //textBox1.Text = textBox1.Text + "LBS: " + wifi.ToString() + " \r\n";
        }

        public static BluetoothDeviceInfo[] bluetoothDiscovery()
        {
            BluetoothDeviceInfo[] arr1 = null;
            BluetoothDeviceInfo[] arr = null;
            try
            {
                BluetoothClient BC = new BluetoothClient();
                //setting bluetooth inquiry time to 5seconds. (initial default is 10 secs)
                BC.InquiryLength = new TimeSpan(0, 0, 5);
                arr1 = BC.DiscoverDevices();
                arr = new BluetoothDeviceInfo[arr1.Length];
                int vrfrCount = 0;
                foreach (BluetoothDeviceInfo b in arr1)
                {
                    String st = b.DeviceName.Trim();
                    if (st.Contains("Pocket_PC"))
                    {
                        arr[vrfrCount] = b;
                        vrfrCount++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return arr;
        }

        public static void bluetoothTestConnection(TextBox textBox1, BluetoothAddress ADDRESS, Byte[] msg)
        {
            try
            {
                BluetoothClient BC = new BluetoothClient();

                Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
                BluetoothEndPoint ep = new BluetoothEndPoint(ADDRESS, MyServiceUuid);
                //textBox1.Text = textBox1.Text + "Conneting...\r\n";
                //textBox1.Text = textBox1.Text + ADDRESS + " \r\n";

                BC.Connect(ep);

                Stream peerStream = BC.GetStream();
                //textBox1.Text = textBox1.Text + "writing... \r\n";
                peerStream.Write(msg, 0, msg.Length);
                byte[] bb1 = new byte[10000];
                //textBox1.Text = textBox1.Text + "reading... \r\n";
                int k = peerStream.Read(bb1, 0, 10000);
                /*
                for (int i = 0; i < k; i++)
                {
                    textBox1.Text = textBox1.Text + Convert.ToChar(bb1[i]);
                }
                */
                peerStream.Flush();
                peerStream.Close();
                BC.Dispose();
                BC.Client.Close();
                BC.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public static void asyncBTDeviceName(BluetoothDeviceInfo b)
        {
            String st = b.DeviceName;
        }

        public static String[] connect(TextBox textBox1, ComboBox loc, ComboBox id)
        {

            String[] delayTimes = new String[7];
            String[] bt = new String[2];
            try
            {
                //textBox1.Text = textBox1.Text + "BT Discovery started..  \r\n ";
                BluetoothClient BC = new BluetoothClient();
                //BluetoothAddress ADDRESS;//= new BluetoothAddress();
                
                //setting bluetooth inquiry time to 5seconds. (initial default is 10 secs)
                BC.InquiryLength = new TimeSpan(0, 0, 5);
                int btDisc1 = System.Environment.TickCount;
                BluetoothDeviceInfo[] arr1 = BC.DiscoverDevices();
                int btDisc2 = System.Environment.TickCount;
                //textBox1.Text = textBox1.Text + "BT Discover Time: " + rtt1.ToString() + " \r\n ";
                int btDisc = btDisc2 - btDisc1;
                String rttBLTH = btDisc.ToString();

                //Second native query to fetch BT device name into BT stack
                /*
                for(int i=2;i<arr1.Length-1;i++)
                {
                    Thread t = new Thread(() => asyncBTDeviceName(arr1[i]));
                    t.Start();
                }
                */
                 
                //signing data before sending to lca
                int sign1 = System.Environment.TickCount;
                string dataString = "claim: 01,12,999,0";
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                byte[] originalData = ByteConverter.GetBytes(dataString);
                byte[] signedData = HashAndSignBytes(originalData, Key);
                int sign2 = System.Environment.TickCount;
                int sign = sign2 - sign1;

                //lca call
                int lca1 = System.Environment.TickCount;
                List<String> response = sendToLCA(textBox1, loc, id, arr1); //all data is sent to lca method as params now
                int lca2 = System.Environment.TickCount;
                int lca = lca2 - lca1;

                //filter only the testing devices

                int vrfrCount = 0;
                BluetoothDeviceInfo[] arr = new BluetoothDeviceInfo[arr1.Length];
                int filter1 = System.Environment.TickCount;
                foreach (BluetoothDeviceInfo b in arr1)
                {
                    String btAddress = b.DeviceAddress.ToString();
                    if (isValidVerifier(btAddress,response))
                    {
                        arr[vrfrCount] = b;
                        vrfrCount++;
                    }
                    
                }
                int filter2 = System.Environment.TickCount;
                int filter = filter2 - filter1;

                //verify lca response
                int verify1 = System.Environment.TickCount;
                bool verfied = clnt.VerifySignedHash(originalData, signedData, Key);
                int verify2 = System.Environment.TickCount;
                int verify = verify2 - verify1;

                String localIP = GetMyIP();
                String trID = response[0];
                Byte[] msg;
                msg = System.Text.Encoding.UTF8.GetBytes(trID);
                int btcon = 0;
                int dat1 = System.Environment.TickCount;
                //String [] thrds = new String[arr.Length];
                //int thr =0;
                foreach (BluetoothDeviceInfo b in arr)
                {
                    if (b == null)
                        continue;
                    BluetoothAddress ADDRESS = b.DeviceAddress;

                    //textBox1.Text = textBox1.Text + "Found Bluetooth DeviceName: '" + b.DeviceName + "'\r\n";
                    String st = b.DeviceName.Trim();
                    
                    int btcon1 = System.Environment.TickCount;
                    bt = btConnection(ADDRESS,msg);
                    //Thread t = new Thread(() => btConnection(ADDRESS, msg));          // Kick off a new thread
                    //t.Start();
                    int btcon2 = System.Environment.TickCount;
                    btcon = btcon2 - btcon1;
                    //thrds[thr++] = btcon1.ToString();
                }

                int dat2 = System.Environment.TickCount;
                int allBtConns = dat2 - btDisc1;
                //for (int i = 0; i < thrds.Length;i++ )
                //  textBox1.Text = textBox1.Text + "Thread" + i + ": " + thrds[i] + "\r\n";
                //textBox1.Text = textBox1.Text + "filter: " + filter.ToString() + "\r\n";
                //textBox1.Text = textBox1.Text + "signing: " + sign.ToString() + "\r\n";
                //textBox1.Text = textBox1.Text + "verify: " + verify.ToString() + "\r\n";
                //textBox1.Text = textBox1.Text + "1 BT request: " + btcon.ToString() + "\r\n";

                delayTimes[0] = rttBLTH;//BT DISC time
                delayTimes[1] = lca.ToString();//lca RTT time
                delayTimes[2] = bt[0];//btConnTime
                delayTimes[3] = bt[1];//btRTTtime
                delayTimes[4] = sign.ToString();//sign
                delayTimes[5] = verify.ToString();//verify
                delayTimes[6] = filter.ToString();//verify            
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error..... " + ex.StackTrace);
                //return null;
                delayTimes[0] = ex.ToString();//BT DISC time
            }
            return delayTimes;
        }

        private static bool isValidVerifier(string btAddress, List<string> response)
        {
            foreach (String res in response)
            {
                if (res == btAddress)
                    return true;
            }
            return false;
        }

        public static String[] btConnection(BluetoothAddress ADDRESS, Byte[] msg)
        {
            //List<String> timeStatList = new List<String>();
            //int start = System.Environment.TickCount;
            String[] bt = null;
            try
            {

                BluetoothClient BC = new BluetoothClient();

                //BluetoothEndPoint ep = new BluetoothEndPoint(ADDRESS, BluetoothService.SerialPort);
                //System.Guid.NewGuid();
                Guid MyServiceUuid = new Guid("a7d21339-7cee-43b1-ad2c-7236880dfd38");
                BluetoothEndPoint ep = new BluetoothEndPoint(ADDRESS, MyServiceUuid);
                //textBox1.Text = textBox1.Text + "Conneting...\r\n";
                int dat5 = System.Environment.TickCount;
                BC.Connect(ep);
                int dat6 = System.Environment.TickCount;
                int btConnTime = dat6 - dat5;

                //textBox1.Text = textBox1.Text + " BT connection time: " + ac.ToString() + "\r\n";
                //textBox1.Text = textBox1.Text + "Connected: \t " + BC.Connected + "\r\n";


                //string cmd = "$PASHS,RID";
                //textBox1.Text = textBox1.Text + "Sending verification request to " + b.DeviceName + " \r\n";
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
                int dat3 = System.Environment.TickCount;
                Stream peerStream = BC.GetStream();
                lock (_locker)
                {
                    peerStream.Write(msg, 0, msg.Length);
                }
                byte[] bb1 = new byte[10000];
                int k = peerStream.Read(bb1, 0, 10000);
                int dat4 = System.Environment.TickCount;
                int btRTTtime = dat4 - dat3;
                //textBox1.Text = textBox1.Text + "1RTT for BT request: " + a.ToString() + "\r\n";
                /*
                for (int i = 0; i < k; i++)
                {
                    textBox1.Text = textBox1.Text + Convert.ToChar(bb1[i]);
                }
                */

                peerStream.Close();
                BC.Client.Close();

                //collect the total times into array
                bt = new String[] { btConnTime.ToString(), btRTTtime.ToString() };

            }
            catch (Exception ex)
            {
                bt[0] = ex.ToString();
                //timeStatList.Add(ex.ToString());
                //textBox1.Text = textBox1.Text + "Error: " + ex.StackTrace + "\r\n";
                //textBox1.Text = textBox1.Text + "Error..... \r\n";
            }
            //int stop = System.Environment.TickCount;
            /*
            timeStatList.Add(start.ToString());
            timeStatList.Add(stop.ToString());
            Random r = new Random();
            report.writeToFile(timeStatList,r.Next(100)+"btTest.txt",false);
            */
            return bt;
        }

        public static String recieve(TextBox textBox1)
        {
            try
            {
                //
                //String localIP = Dns.GetHostEntry(Dns.GetHostName()).ToString();
                String localIP = GetMyIP();
                //String localIP = ((serverSocket.getInetAddress()).getLocalHost()).getHostAddress();
                //textBox1.Text = textBox1.Text + "ClaimerIP: "+localIP+" waiting for LCA response.. \r\n";
                IPAddress ipAd = IPAddress.Parse(localIP); //use local m/c IP address, and use the same in the client
                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 8001);//use same port in LBS server to communicate
                /* Start Listeneting at the specified port */
                myList.Start();
                //Console.WriteLine("The server is running at port 8001...");
                //Console.WriteLine("The local End point is :" + myList.LocalEndpoint);
                //Console.WriteLine("Waiting for a connection.....");
                Socket s = myList.AcceptSocket();
                //textBox1.Text = textBox1.Text + "Message from LBS: " + s.RemoteEndPoint + " \r\n";
                byte[] b = new byte[100];
                int k = s.Receive(b);
                //textBox1.Text = textBox1.Text + "Recieved... \r\n";
                String lcaResponse = "";
                for (int i = 0; i < k; i++)
                    lcaResponse = lcaResponse + Convert.ToChar(b[i]);
                //textBox1.Text = textBox1.Text + Convert.ToChar(b[i]);
                //textBox1.Text = textBox1.Text + " \r\n";
                ASCIIEncoding asen = new ASCIIEncoding();
                //s.Send(asen.GetBytes("The string was recieved by the server."));
                //Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();
                return lcaResponse;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error..... " + e.StackTrace);
                return null;
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

        public static void testRSA1(TextBox textBox1)
        {
            try
            {
                // Create a UnicodeEncoder to convert between byte array and string.
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                textBox1.Text = "writing..";
                string dataString = "claim: 01,12,999,0";

                StreamWriter SW;
                SW = File.CreateText("File.txt");
                long i = 0;
                /* while (i++ < 10000)
                 {
                     //textBox1.Text = "i=" + i++;
                     dataString = dataString + "God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ... God is ... God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...God is ... God is ...\r\n";
                 }*/
                //SW.WriteLine("This is second line");
                SW.Close();


                // create reader & open file
                /*
                 textBox1.Text = "reading..";
                 StreamReader SR;
                 String S;
                 SR = File.OpenText("File.txt");
                 S = SR.ReadLine();
                 long j = 0;
                 while (S != null)
                 {
                     textBox1.Text = "j=" + j++;
                     S = SR.ReadLine();
                     dataString = dataString + S;
                 }
                 * */
                textBox1.Text = "signing..";
                //textBox1.Text = textBox1.Text + "Input String: "+dataString+" \r\n";
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
                //Stopwatch stopWatch = Stopwatch.StartNew();
                int dat1 = System.Environment.TickCount;
                //signedData = HashAndSignBytes(originalData, Key);
                int dat2 = System.Environment.TickCount;
                int dat4 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat4 - dat1));
                textBox1.Text = textBox1.Text + "originalData Size: " + originalData.Length + ", Time taken for signing: " + a.ToString() + "\r\n Signed data: " + " \r\n";
                /*
                for (int i = 0; i < signedData.Length;i++ )
                {
                    textBox1.Text = textBox1.Text + signedData[i];
                }
                */
                //textBox1.Text = textBox1.Text + " \r\n";
                MessageBox.Show("ok");
                // Verify the data and display the result to the 
                // console.
                int dat3 = System.Environment.TickCount;
                /*if (VerifySignedHash(originalData, signedData, Key))
                {
                    int dat4 = System.Environment.TickCount;
                    StringBuilder b = new StringBuilder();
                    b.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat4 - dat3));

                    textBox1.Text = textBox1.Text + "The data was verified in time: " + b.ToString() + " \r\n";
                }
                else
                {
                    textBox1.Text = textBox1.Text + "The data does not match the signature." + " \r\n";
                }*/

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The data was not signed or verified");

            }
        }

        public static void sendToServer(TextBox textBox1, string m, string exp, string s, string org)
        {
            try
            {
                //serverIP = ServerIPAddress.Text.ToString().Trim();
                //textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n";
                TcpClient tcpclnt = new TcpClient();
                //Console.WriteLine("Connecting.....");
                //tcpclnt.Connect(IPAddress.Parse(serverIP), 8000); // use the ipaddress as in the server program
                tcpclnt.Connect(serverIP, 8000); // use the hostname of the server program

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ln = asen.GetBytes("Manoop");

                Stream stm = tcpclnt.GetStream();
                //string mod = System.Text.ASCIIEncoding.ASCII.GetString(m, 0, m.Length);
                textBox1.Text = textBox1.Text + "s: " + s + "\r\n";
                byte[] bs = asen.GetBytes(m);
                stm.Write(bs, 0, bs.Length);
                stm.Write(ln, 0, ln.Length);

                byte[] e = asen.GetBytes(exp);
                stm.Write(e, 0, e.Length);
                stm.Write(ln, 0, ln.Length);

                byte[] sig = asen.GetBytes(s);
                stm.Write(sig, 0, sig.Length);
                stm.Write(ln, 0, ln.Length);

                byte[] orig = asen.GetBytes(org);
                stm.Write(orig, 0, orig.Length);
                stm.Write(ln, 0, ln.Length);

                tcpclnt.Close();
                stm.Close();
                //connect(textBox1,trID.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error..... " + ex.StackTrace);
            }
        }

        public static void testRSA(TextBox textBox1)
        {
            try
            {
                // Create a UnicodeEncoder to convert between byte array and string.
                ASCIIEncoding ByteConverter = new ASCIIEncoding();

                string dataString = "claim: 01,12,999,0";


                // Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] originalData = ByteConverter.GetBytes(dataString);
                byte[] signedData;

                // Create a new instance of the RSACryptoServiceProvider class 
                // and automatically create a new key-pair.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1024);

                // Export the key information to an RSAParameters object.
                // You must pass true to export the private key for signing.
                // However, you do not need to export the private key
                // for verification.
                RSAParameters Key = RSAalg.ExportParameters(true);
                textBox1.Text = textBox1.Text + "alg: " + RSAalg.KeyExchangeAlgorithm + " : " + RSAalg.SignatureAlgorithm;
                textBox1.Text = textBox1.Text + "Modulus: " + Key.Modulus.Length + "\r\n";
                string mod = "";
                mod = System.Text.ASCIIEncoding.ASCII.GetString(Key.Modulus, 0, Key.Modulus.Length);
                /*for (int i = 0; i < Key.Modulus.Length; i++)
                {
                    //textBox1.Text = textBox1.Text + Key.Modulus[i];
                    mod = mod + Key.Modulus[i];
                }*/

                textBox1.Text = "\r\n" + textBox1.Text + "Exp: " + Key.Exponent.Length + "\r\n";
                string exp = "";
                exp = System.Text.ASCIIEncoding.ASCII.GetString(Key.Exponent, 0, Key.Exponent.Length);
                /*for (int i = 0; i < Key.Exponent.Length;i++ )
                {
                    //textBox1.Text = textBox1.Text + Key.Exponent[i];
                    exp = exp + Key.Exponent[i];
                }*/

                // Hash and sign the data.
                //Stopwatch stopWatch = Stopwatch.StartNew();
                int dat1 = System.Environment.TickCount;
                signedData = HashAndSignBytes(originalData, Key);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                textBox1.Text = textBox1.Text + "originalData Size: " + originalData.Length + ", Time taken for signing: " + a.ToString() + "\r\n Signed data: " + " \r\n";

                string sig = "";
                sig = System.Text.ASCIIEncoding.ASCII.GetString(signedData, 0, signedData.Length);
                /*for (int i = 0; i < signedData.Length; i++)
                {
                    //textBox1.Text = textBox1.Text + Key.Exponent[i];
                    sig = sig + signedData[i];
                }*/
                textBox1.Text = textBox1.Text + "org data \r\n";
                string org = "";
                org = System.Text.ASCIIEncoding.ASCII.GetString(originalData, 0, originalData.Length);
                /*for (int i = 0; i < originalData.Length; i++)
                {
                    textBox1.Text = textBox1.Text + originalData[i];
                    org = org + originalData[i];
                }*/


                sendToServer(textBox1, mod, exp, sig, org);
                /*
                for (int i = 0; i < signedData.Length;i++ )
                {
                    textBox1.Text = textBox1.Text + signedData[i];
                }
                */
                //textBox1.Text = textBox1.Text + " \r\n";

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
            catch (Exception)
            {
                //Console.WriteLine("The data was not signed or verified");
                textBox1.Text = textBox1.Text + "Error!!";
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
}
