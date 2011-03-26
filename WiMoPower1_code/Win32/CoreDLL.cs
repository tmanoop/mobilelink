using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Win32
{
    public static class CoreDLL
    {
        [DllImport("CoreDLL")]
        public static extern int ReleasePowerRequirement(IntPtr hPowerReq);


        [DllImport("CoreDLL", SetLastError=true)]
        public static extern IntPtr SetPowerRequirement
        (
            string pDevice,
            CEDEVICE_POWER_STATE DeviceState,
            DevicePowerFlags DeviceFlags,
            IntPtr pSystemState,
            uint StateFlagsZero
        );

        [DllImport("CoreDLL", SetLastError = true)]
        public static extern IntPtr SetDevicePower
            (
                string pDevice,
                DevicePowerFlags DeviceFlags,
            CEDEVICE_POWER_STATE DevicePowerState
            );
        [DllImport("CoreDLL")]
        public static extern int GetDevicePower(string device, DevicePowerFlags flags, out CEDEVICE_POWER_STATE PowerState);

        [DllImport("CoreDLL")]
        public static extern int SetSystemPowerState(String stateName, PowerState powerState, DevicePowerFlags flags);


        [DllImport("CoreDLL")]
        public static extern int PowerPolicyNotify(
          PPNMessage dwMessage,
            int option
        //    DevicePowerFlags);
        );

        [DllImport("CoreDLL")]
        public static extern int GetSystemPowerStatusEx2(
             SYSTEM_POWER_STATUS_EX2 statusInfo, 
            int length,
            int getLatest
                );

        
        public static SYSTEM_POWER_STATUS_EX2 GetSystemPowerStatus()
        {
            SYSTEM_POWER_STATUS_EX2 retVal = new SYSTEM_POWER_STATUS_EX2();
           int result =  GetSystemPowerStatusEx2( retVal, Marshal.SizeOf(retVal) , 1);
            return retVal;
        }
        // System\CurrentControlSet\Control\Power\Timeouts
        [DllImport("CoreDLL")]
        public static extern int SystemParametersInfo
        (
            SPI Action,
            uint Param, 
            ref int  result, 
            int updateIni
        );

        [DllImport("CoreDLL")]
        public static extern int SystemIdleTimerReset();

        [DllImport("CoreDLL")]
        public static extern int CeRunAppAtTime(string application, SystemTime startTime);
        [DllImport("CoreDLL")]
        public static extern int CeRunAppAtEvent(string application, int EventID);

        [DllImport("CoreDLL")]
        public static extern int FileTimeToSystemTime(ref long lpFileTime, SystemTime lpSystemTime);
        [DllImport("CoreDLL")]
        public static extern int FileTimeToLocalFileTime(ref long lpFileTime, ref long lpLocalFileTime);

        // For named events
        //[DllImport("CoreDLL", SetLastError = true)]
        //internal static extern bool EventModify(IntPtr hEvent, EVENT ef);

        //[DllImport("CoreDLL", SetLastError = true)]
        //internal static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        //[DllImport("CoreDLL", SetLastError = true)]
        //internal static extern bool CloseHandle(IntPtr hObject);

        //[DllImport("CoreDLL", SetLastError = true)]
        //internal static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

        //[DllImport("CoreDLL", SetLastError = true)]
        //internal static extern int WaitForMultipleObjects(int nCount, IntPtr[] lpHandles, bool fWaitAll, int dwMilliseconds);
    }
}
