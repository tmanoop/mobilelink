using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;

namespace SmartDeviceProject1
{
    class BluetoothCall
    {
        static RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
        static RSAParameters Key = RSAalg.ExportParameters(true);

        static String serverIP = "manoop.dyndns.org";

        public int start = 0;
        public int stop = 0;
        public int lbs = 0;
        public int btDisc = 0;
        public int btDisc1 = 0;
        public int btDisc2= 0;
        public int sign = 0;
        public int lca = 0;
        public int verify = 0;
        public int btAll = 0;
        public String[] bt = new String[2];
        public int lbsResp = 0;
        public int linkRTT = 0;

        public TextBox textBox1 = null;
        public ComboBox loc = null;
        public ComboBox id = null;
        public String trID = "1";
        public Byte[] msg = null;
        public int vrfrCount = 10;
        public BluetoothCall()
        {
        }
        public BluetoothCall(TextBox tb1, ComboBox location, ComboBox identity)
        {
            textBox1 = tb1;
            loc = location;
            id = identity;
        }
        public void bluetoothDiscoveryAsynch()
        {
            btDisc1 = System.Environment.TickCount;
            //textBox1.Text = textBox1.Text + "Begin BT async" + " \r\n";
            try
            {
                //bool discoOnly, auth, rembd, unk;
                BluetoothClient BC = new BluetoothClient();
                BC.InquiryLength = new TimeSpan(0, 0, 5);
                BluetoothComponent bco = new BluetoothComponent(BC);
                //BCO.DiscoverDevicesAsync(255,false,true,false,true,99);
                bco.DiscoverDevicesProgress += _DiscoDevicesAsync_ProgressCallback;
                bco.DiscoverDevicesComplete += _DiscoDevicesAsync_CompleteCallback;
                bco.DiscoverDevicesAsync(255, false, true, false, true, 99);
            }
            catch (Exception ex)
            {
                //catch ex
            }
            //textBox1.Text = textBox1.Text + "End BT async" + " \r\n";
        }

        void _DiscoDevicesAsync_CompleteCallback(object sender, DiscoverDevicesEventArgs e)
        {
            btDisc2 = System.Environment.TickCount;
            btDisc = btDisc2 - btDisc1;
            //textBox1.Text = textBox1.Text + "btDisc: " + btDisc.ToString();
            //textBox1.Text = textBox1.Text + "Begin completeCallBack" + " \r\n";
            if (WriteAsyncCompletedState("DiscoDevicesAsync", e))
            {
                vrfrCount = e.Devices.Length;
                //textBox1.Text = textBox1.Text + "DiscoDevicesAsync found devices: " + e.Devices.Length + " \r\n";
                
                //textBox1.Text = textBox1.Text + "LCA RTT: " + lca.ToString();
                sendVrfrsCountToLCA(vrfrCount);
                

                int lbsResp1 = System.Environment.TickCount;
                String lbsResponse = clnt.recieve(textBox1);
                int lbsResp2 = System.Environment.TickCount;
                lbsResp = lbsResp2 - lbsResp1;
                stop = System.Environment.TickCount;
                linkRTT = stop - start;
                printTimes();
                writeToFile();
                
                //_DiscoDevicesAsync_ShowDevices(e);
            }
            //textBox1.Text = textBox1.Text + "Complete." + " \r\n";
        }

        void printTimes()
        {
            textBox1.Text = textBox1.Text + "lbs: " + lbs.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "btDisc: " + btDisc.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "sign: " + sign.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "lca: " + lca.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "verify: " + verify.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "btAll: " + btAll.ToString() + " \r\n";
            //textBox1.Text = textBox1.Text + "btConn: " + bt[0].ToString() + " \r\n";
            //textBox1.Text = textBox1.Text + "btRTT: " + bt[1].ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "lbsResp: " + lbsResp.ToString() + " \r\n";
            textBox1.Text = textBox1.Text + "linkRTT: " + linkRTT.ToString() + " \r\n";
        }

        void writeToFile()
        {
            // Create a new List
            List<String> timeStatList = new List<String>();

            String[] linkResponseTimes = new String[10];
            linkResponseTimes[0] = lbs.ToString();
            linkResponseTimes[1] = btDisc.ToString();
            linkResponseTimes[2] = lca.ToString();
            linkResponseTimes[3] = bt[0];
            linkResponseTimes[4] = bt[1];
            linkResponseTimes[5] = btAll.ToString();
            linkResponseTimes[6] = sign.ToString();
            linkResponseTimes[7] = verify.ToString();
            linkResponseTimes[8] = lbsResp.ToString();
            linkResponseTimes[9] = linkRTT.ToString();

            String test = "";
            for (int i = 1; i < linkResponseTimes.Length; i++)
            {
                test = test + linkResponseTimes[i] + ",";
            }
            timeStatList.Add(test);
            report.writeToFile(timeStatList, "LinkRTTasync.txt", false);
        }

        void sendVrfrsCountToLCA(int vrfrCount)
        {
            try
            {
                //textBox1.Text = textBox1.Text + "Sending Location claim to LCA.. \r\n" + GetMyIP();
                TcpClient tcpclnt = new TcpClient();
                //tcpclnt.Connect(IPAddress.Parse(serverIP), 8000); // use the ipaddress as in the server program
                tcpclnt.Connect(serverIP, 8000); // use the hostname of the server program

                String str = "verifiersCount," + trID.Trim() + "," + vrfrCount +", \n";
                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                
                int dat1 = System.Environment.TickCount;
                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                int dat2 = System.Environment.TickCount;
                StringBuilder a = new StringBuilder();
                a.AppendFormat(new System.Globalization.NumberFormatInfo(), "{0}", (dat2 - dat1));
                
            }
            catch (Exception ex)
            {
                //
            }
        }

        void _DiscoDevicesAsync_ProgressCallback(object sender, DiscoverDevicesEventArgs e)
        {
            //String[] bt = new String[2];
            int btAll1 = System.Environment.TickCount;
            foreach (var dev in e.Devices)
            {
                //textBox1.Text = textBox1.Text + "last seen device Address: " + dev.DeviceAddress + " Name: " + dev.DeviceName + " at: " + System.Environment.TickCount + " \r\n";
                //textBox1.Text = textBox1.Text + "Device Name: " + dev.DeviceName + " \r\n";
                msg = System.Text.Encoding.UTF8.GetBytes(trID);
                String st = dev.DeviceName.Trim();
                if (st.Contains("Pocket_PC"))
                {
                    bt = clnt.btConnection(dev.DeviceAddress, msg);
                }
            }
            int btAll2 = System.Environment.TickCount;
            btAll = btAll2 - btAll1;
        }

        void _DiscoDevicesAsync_ShowDevices(DiscoverDevicesEventArgs e)
        {
            foreach (var dev in e.Devices)
            {
                textBox1.Text = textBox1.Text + "last seen device Address: " + dev.DeviceAddress + " Name: " + dev.DeviceName + " at: " + System.Environment.TickCount + " \r\n";
            }
        }

        bool WriteAsyncCompletedState(string name,
#if NETCF // && !FX3_5
            InTheHand.Net.Bluetooth.AsyncCompletedEventArgs e
#else
 AsyncCompletedEventArgs e
#endif
)
        {
            if (e.Cancelled)
            {
                //textBox1.Text = textBox1.Text + "Cancelled: " + name + " \r\n";
            }
            else if (e.Error != null)
            {
                //textBox1.Text = textBox1.Text + "Error in: " + name + " \r\n";
            }
            else
            {
                //textBox1.Text = textBox1.Text + "Successful Completion of: " + name + " \r\n";
                return true;
            }
            return false;
        }

        public void connect(TextBox textBox1, ComboBox loc, ComboBox id)
        {
            start = System.Environment.TickCount;
            
            try
            {
                //send to lbs
                int lbs1 = System.Environment.TickCount;
                clnt.sendToLBS(textBox1);
                int lbs2 = System.Environment.TickCount;
                lbs = lbs2 - lbs1;
                
                //send to lca
                int lca1 = System.Environment.TickCount;
                //fix this call before testing
                //trID = clnt.sendToLCA(textBox1, loc, id, vrfrCount); //all data is sent to lca method as params now
                int lca2 = System.Environment.TickCount;
                lca = lca2 - lca1;

                //signing data before sending to lca
                int sign1 = System.Environment.TickCount;
                string dataString = "claim: 01,12,999,0";
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                byte[] originalData = ByteConverter.GetBytes(dataString);
                byte[] signedData = clnt.HashAndSignBytes(originalData, Key);
                int sign2 = System.Environment.TickCount;
                sign = sign2 - sign1;

                //verify lca response
                int verify1 = System.Environment.TickCount;
                bool verfied = clnt.VerifySignedHash(originalData, signedData, Key);
                int verify2 = System.Environment.TickCount;
                verify = verify2 - verify1;

                //start bt calls
                bluetoothDiscoveryAsynch();
                
            }
            catch (Exception ex)
            {
                //
            }
        }

    }
}
