using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Microsoft.Win32;

namespace MyPowerStates
{
    public partial class Form1 : Form
    {
        const string BASE_POWER_HIVE = @"System\CurrentControlSet\Control\Power\State";
        string[] _powerStateNames = {"Full Power","Power Savings","Standby","Sleep Mode","Power Off"};

        Regex _targetRegistryValue = new Regex("(DEFAULT)|(^.*:$)", RegexOptions.IgnoreCase);
        public Form1()
        {
            InitializeComponent();
        }

        string[] GetPowerStateList()
        {
            RegistryKey powerStateKey = Registry.LocalMachine.OpenSubKey(BASE_POWER_HIVE);
            return powerStateKey.GetSubKeyNames();
        }

        string[][] GetPowerStateInfo(string stateName)
        {
            RegistryKey stateInformationKey = Registry.LocalMachine.OpenSubKey(String.Format(@"{0}\{1}", BASE_POWER_HIVE, stateName));
            string[] valueList = stateInformationKey.GetValueNames();
            List<string[]> StateInfo = new List<string[]>();
            for (int i = 0; i < valueList.Length; ++i)
            {
                string currentValue = valueList[i];
                if (_targetRegistryValue.IsMatch(currentValue))
                {
                    StateInfo.Add(new string[] { valueList[i],_powerStateNames[(int) stateInformationKey.GetValue(currentValue)]});
                }
            }
            return StateInfo.ToArray();
        }

        void PopulatePowerState()
        {
            cboPowerState.Items.Clear();
            string[] stateList = GetPowerStateList();
            List<string> sortList = new List<string>(stateList);
            sortList.Sort();

            for (int i = 0; i < sortList.Count; ++i)
            {
                cboPowerState.Items.Add(sortList[i]);
            }
        }

        void PopulatePowerDetails(string powerStateName)
        {
            lstPowerStateInfo.Items.Clear();
            string[][] settingList = GetPowerStateInfo(powerStateName);
            for (int i = 0; i < settingList.Length; ++i)
            {
                lstPowerStateInfo.Items.Add(new ListViewItem(settingList[i]));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePowerState();
        }

        private void cboPowerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePowerDetails(cboPowerState.Text);
        }

        private void lstPowerStateInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstPowerStateInfo.Items.Clear();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}