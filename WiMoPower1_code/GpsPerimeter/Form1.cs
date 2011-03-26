using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Win32;
using Microsoft.WindowsMobile.Samples.Location;
using Microsoft.Win32;

namespace GpsPerimeter
{
    public partial class Form1 : Form
    {

        const double EARTH_RADIUS_MILES = 3956d;
        const double EARTH_RADIUS_KILOMETER = 6371000;
        bool _alarmTriggered = false;
        double alarmDistance = 250;

        Gps _gpsDevice;
        string _gpsDeviceName = null;
        IntPtr _gpsPowerRequirements = IntPtr.Zero;

        double[] realDistance = { 50,100,250,1000,2000,5000};

        GpsPosition _targetPosition;
        GpsPosition _currentPosition;
        
        delegate void setTextDelegate(Control c, String s);
        delegate void setHeadingDelegate(double angle);

        setTextDelegate setText = null;
        setHeadingDelegate setHeading = null;


        const string GPS_DEVICE_NAME_PATH = "DRIVERS\\Builtin\\GPSID";
        public string GpsDeviceName
        {
            get
            {
                if (_gpsDeviceName == null)
                {
                    RegistryKey gpsInfoKey = Registry.LocalMachine.OpenSubKey(GPS_DEVICE_NAME_PATH);
                    if (gpsInfoKey != null)
                    {
                        try
                        {
                            _gpsDeviceName = String.Format("{0}{1}:", gpsInfoKey.GetValue("Prefix"), gpsInfoKey.GetValue("Index"));
                        }
                        catch { }
                    }
                }
                return _gpsDeviceName;
            }
        }
        public string ExecutingFolder
        {
            get
            {
                string assemblyPath = this.GetType().Assembly.GetModules()[0].FullyQualifiedName;
                string assemblyFolder = assemblyPath.Substring(0, assemblyPath.LastIndexOf("\\"));
                return assemblyFolder;
            }
        }
        


        string DistanceString(double distance)
        {
            if (distance > 900)
                return String.Format(String.Format("{0:0.000} km", distance / 1000d));
            return String.Format("{0:0} m", distance);
        }
        void SetControlText(Control c, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(setText, new object[] { c, text });
            }
            else
                c.Text = text;
        }

        void SetHeading(double heading)
        {
            if (this.InvokeRequired)
            {
                Invoke(setHeading, new object[] { heading });
            }
            else
            {
                compassControl.Angle = heading;
            }
        }

        void AlertUser()
        {
            if (!_alarmTriggered)
            {
                _alarmTriggered=true;
                SetControlText(lblAlarmMessage, "Outside of Perimeter");
                string soundPath = Path.Combine(ExecutingFolder, "Alarm.mp3");
                Aygshell.PlaySound(soundPath);
            }

        }

        public Form1()
        {
            InitializeComponent();

            CoreDLL.PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, -1);
            setText = new setTextDelegate(this.SetControlText);
            setHeading = new setHeadingDelegate(this.SetHeading);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CEDEVICE_POWER_STATE currentPowerState;
            CoreDLL.GetDevicePower(GpsDeviceName, DevicePowerFlags.POWER_NAME, out currentPowerState);
            _gpsPowerRequirements = CoreDLL.SetPowerRequirement(GpsDeviceName, CEDEVICE_POWER_STATE.D0, DevicePowerFlags.POWER_NAME, IntPtr.Zero, 0);
            if (currentPowerState == CEDEVICE_POWER_STATE.D4)
                System.Threading.Thread.Sleep(500);
            _gpsDevice = new Gps();
            _gpsDevice.Open();
            _gpsDevice.LocationChanged += new LocationChangedEventHandler(_gpsDevice_LocationChanged);
            _gpsDevice.DeviceStateChanged += new DeviceStateChangedEventHandler(_gpsDevice_DeviceStateChanged);
            this.cboDistance.SelectedIndex = 2;

        }

        void _gpsDevice_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
        {
            if (args.DeviceState.ServiceState != GpsServiceState.On)
            {
                SetControlText(lblCurrentLocation, args.DeviceState.ServiceState.ToString());
            }
        }

        void _gpsDevice_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            _currentPosition = args.Position;
            if ((args.Position.LatitudeValid && args.Position.LongitudeValid) && (_targetPosition != null))
            {                
                double distance = DistanceCalculatoor.CalcDistance(_targetPosition.Latitude, _targetPosition.Longitude, _currentPosition.Latitude, _currentPosition.Longitude, EARTH_RADIUS_KILOMETER);
                if (distance > alarmDistance)
                    AlertUser();
                
                SetControlText(lblDistance, DistanceString(distance));
                SetControlText(lblCurrentLocation, _currentPosition.ToString());
                
                SetControlText(lblTravelDirection, _currentPosition.Heading.ToString());
                SetHeading(_currentPosition.Heading);
            }
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            _gpsDevice.Close();
            CoreDLL.PowerPolicyNotify(PPNMessage.PPN_UNATTENDEDMODE, 0);
            CoreDLL.ReleasePowerRequirement(_gpsPowerRequirements);
        }


        private void menuItem1_Click(object sender, EventArgs e)
        {
            _targetPosition = _currentPosition;
            _alarmTriggered = false;
            SetControlText(lblAlarmMessage, String.Empty);
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            alarmDistance = realDistance[cboDistance.SelectedIndex];

        }

    }

}