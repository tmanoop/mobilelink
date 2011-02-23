﻿using System;

using System.Collections.Generic;
using System.Text;

namespace Win32
{
    public enum SPI : uint
    {
        /*
        SPI_GETMOUSE = 3,
        SPI_SETMOUSE = 4,
        SPI_SETDESKWALLPAPER = 20,
        SPI_SETDESKPATTERN = 21,

        SPI_SETWORKAREA = 47,
        SPI_GETWORKAREA = 48,
        SPI_GETSHOWSOUNDS = 56,
        SPI_SETSHOWSOUNDS = 57,

        SPI_GETDEFAULTINPUTLANG = 89,

        SPI_SETLANGTOGGLE = 91,

        SPI_GETWHEELSCROLLLINES = 104,
        SPI_SETWHEELSCROLLLINES = 105,

        SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,
        SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,

        SPI_GETFONTSMOOTHING = 0x004A,
        SPI_SETFONTSMOOTHING = 0x004B,
        SPI_GETSCREENSAVETIMEOUT = 14,
        SPI_SETSCREENSAVETIMEOUT = 15,
        */
        SPI_SETBATTERYIDLETIMEOUT = 251,
        SPI_GETBATTERYIDLETIMEOUT = 252,

        SPI_SETEXTERNALIDLETIMEOUT = 253,
        SPI_GETEXTERNALIDLETIMEOUT = 254,

        SPI_SETWAKEUPIDLETIMEOUT = 255,
        SPI_GETWAKEUPIDLETIMEOUT = 256
    }
}