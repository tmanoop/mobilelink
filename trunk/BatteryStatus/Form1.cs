using System;

using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32;
using System.Reflection;

namespace BatteryStatus
{
    public partial class Form1 : Form
    {
        Assembly _resourceAssembly;
        Dictionary<ACLineStatus, Image> _acImageDictionary;
        Dictionary<BatteryFlag, Image> _batteryImageDictionary;
        Dictionary<BatteryChemistry, string> _batteryNameDictionary;

        SYSTEM_POWER_STATUS_EX2 _prevStatus = null;
        SYSTEM_POWER_STATUS_EX2 _currentStatus = null;
        bool _enableRefresh = true;
        bool _hasFocus = true;
        // Create a new List
        List<PowerStat> powerStatList = new List<PowerStat>();

        public Form1()
        {
            InitializeComponent();
            _resourceAssembly = this.GetType().Assembly;
            _acImageDictionary = new Dictionary<ACLineStatus, Image>();
            _batteryImageDictionary = new Dictionary<BatteryFlag, Image>(6);
            _batteryNameDictionary = new Dictionary<BatteryChemistry, string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadImages();
            UpdateDisplay();

        }

        void AppendBatteryStatus(ListViewItem lv)
        {
            this.lstBatteryStats.Items.Add(lv);
        }

        void UpdateDisplay()
        {
            _prevStatus = _currentStatus;
            _currentStatus = CoreDLL.GetSystemPowerStatus();
            DisplayBatteryStatus();
            

        }

        Image LoadResourceImage(string resourceName)
        {
            string target = String.Format("BatteryStatus.Images.{0}", resourceName);
            Stream s = _resourceAssembly.GetManifestResourceStream(target);
            Bitmap bm = new Bitmap(s);
            return bm;
        }
        void LoadImages()
        {            
            _acImageDictionary.Add(ACLineStatus.AC_LINE_OFFLINE, LoadResourceImage("AC_Offline.bmp"));
            _acImageDictionary.Add(ACLineStatus.AC_LINE_ONLINE, LoadResourceImage("AC_Online.bmp"));
            _acImageDictionary.Add(ACLineStatus.AC_LINE_UNKNOWN, LoadResourceImage("AC_Unknown.bmp"));
            _acImageDictionary.Add(ACLineStatus.AC_LINE_BACKUP_POWER, LoadResourceImage("AC_Backup.bmp"));

            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_CRITICAL, LoadResourceImage("Battery_Critical.bmp"));
            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_CHARGING, LoadResourceImage("Battery_Charging.bmp"));
            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_NO_BATTERY, LoadResourceImage("Battery_No_Battery.bmp"));
            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_UNKNOWN, LoadResourceImage("Battery_Unknown.bmp"));
            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_LOW, LoadResourceImage("Battery_Low.bmp"));
            _batteryImageDictionary.Add(BatteryFlag.BATTERY_FLAG_HIGH, LoadResourceImage("Battery_High.bmp"));

            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_ALKALINE, "AK");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_LION, "Li+");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_LIPOLY, "LiP");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_NICD, "NiCD");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_NIMH, "NiMH");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_UNKNOWN, "?");
            _batteryNameDictionary.Add(BatteryChemistry.BATTERY_CHEMISTRY_ZINCAIR, "Zn");



        }

        void DisplayBatteryStatus()
        {

            PowerStat ps = new PowerStat();
            ps.setTimestamp(System.DateTime.Now.ToLongTimeString());

            if ((_prevStatus == null) || (_prevStatus.ACLineStatus != _currentStatus.ACLineStatus))
            {
                pbACLine.Image = _acImageDictionary[_currentStatus.ACLineStatus];
            }
            if ((_prevStatus == null) || (_prevStatus.BackupBatteryFlag != _currentStatus.BackupBatteryFlag))
            {
                pbBatteryFlag.Image = _batteryImageDictionary[_currentStatus.BackupBatteryFlag];
            }

            if ((_prevStatus == null) || (_prevStatus.BatteryLifePercent != _currentStatus.BatteryLifePercent))
            {
                txtBatteryPercentage.Text = String.Format("{0:#00}%", _currentStatus.BatteryLifePercent);
            }

            if ((_prevStatus == null) || (_prevStatus.BatteryTemperature != _currentStatus.BatteryTemperature))
            {
                txtTemperature.Text = String.Format("{0:0.0}C", 0.1 * (float)_currentStatus.BatteryTemperature);
            }

            if ((_prevStatus == null) || (_prevStatus.BatteryVoltage != _currentStatus.BatteryVoltage))
            {
                txtBatteryVoltage.Text = String.Format("{0:0.000}v", 0.001 * (float)_currentStatus.BatteryVoltage);
                ps.setVoltage(txtBatteryVoltage.Text);
            }
            if ((_prevStatus == null) || (_prevStatus.BatteryCurrent != _currentStatus.BatteryCurrent))
            {
                txtBatteryCurrent.Text = String.Format("{0:0}ma" , _currentStatus.BatteryCurrent);
                ps.setCurrent(txtBatteryCurrent.Text);
            }
            if ((_prevStatus == null) || (_prevStatus.BatteryChemistry != _currentStatus.BatteryChemistry))
            {
                txtBatteryChem.Text = _batteryNameDictionary[_currentStatus.BatteryChemistry];
            }

            
            //lstBatteryStats.Items.Clear();
            
            if (lstBatteryStats.Items.Count == 0)
            {
                
                AppendBatteryStatus(new ListViewItem(new string[] { "AC Line Status",               String.Empty}));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Status",               String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Backup Battery Full Lifetime", String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Backup Battery Life %",        String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Backup Battery Time",          String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Backup Battery Voltage",       String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Average Current",      String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Average Interval",     String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Chemistry",            String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Current",              String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Flag",                 String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Full Life Time",       String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Life Percent",         String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Life Time",            String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "mAH Consumer",                 String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Temperature",          String.Empty }));
                AppendBatteryStatus(new ListViewItem(new string[] { "Battery Voltage",              String.Empty }));
            }
            int i = 0;
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.ACLineStatus.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BackupBatteryFlag.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BackupBatteryFullLifeTime.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BackupBatteryLifePercent.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BackupBatteryLifeTime.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BackupBatteryVoltage.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatteryAverageCurrent.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text =  _currentStatus.BatteryAverageInterval.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text =  _currentStatus.BatteryChemistry.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text =  _currentStatus.BatteryCurrent.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text =  _currentStatus.BatteryFlag.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text =  _currentStatus.BatteryFullLifeTime.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatteryLifePercent.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatteryLifeTime.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatterymAHourConsumed.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatteryTemperature.ToString();
            lstBatteryStats.Items[i++].SubItems[1].Text = _currentStatus.BatteryVoltage.ToString();

            if (ps.getCurrent() != "" || ps.getVoltage() != "")
            {
                if (ps.getCurrent() == "")
                    ps.setCurrent(txtBatteryCurrent.Text);
                if (ps.getVoltage() == "")
                    ps.setVoltage(txtBatteryVoltage.Text);
                powerStatList.Add(ps);
            }
             
        }

        private void mi1sec_Click(object sender, EventArgs e)
        {
            updateTimer.Interval = 1000;
        }

        private void mi5sec_Click(object sender, EventArgs e)
        {
            updateTimer.Interval = 5000;
        }

        private void mi10sec_Click(object sender, EventArgs e)
        {
            updateTimer.Interval = 10000;
        }

        private void miPause_Click(object sender, EventArgs e)
        {
            _enableRefresh = !_enableRefresh;
            updateTimer.Enabled = _enableRefresh && _hasFocus;
            this.miPause.Text = (updateTimer.Enabled) ? "&Pause" : "&Continue";
        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            updateTimer.Enabled = false;
            this.Close();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            _hasFocus = true;
            updateTimer.Enabled = _enableRefresh;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            _hasFocus = false;
            updateTimer.Enabled = false;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            report.writeToFile(powerStatList);
        }
    }
}