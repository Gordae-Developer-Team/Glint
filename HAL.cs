using System;
using System.Text;
using SYS = Cosmos.System;
using OLDHAL = Cosmos.HAL;

namespace HAL
{
    public class RTC {
        public static int Year = OLDHAL.RTC.Year;
        public static int Month = OLDHAL.RTC.Month;
        public static int Day = OLDHAL.RTC.DayOfTheMonth;

        public static int Hour = OLDHAL.RTC.Hour;
        public static int Minute = OLDHAL.RTC.Minute;
        public static int Second = OLDHAL.RTC.Second;
    }

    public class PCS
    {
        public static void Beep(uint freq)
        {
            SYS.PCSpeaker.Beep(freq);
        }
    }

    public class KernelException
    {
        public static void InvalidDate()
        {
            throw new Exception("The Date Is Invalid.");
        }

        public static void ProcessDied()
        {
            throw new Exception("Critical Process Does Not Exist!");
        }
    }

    public class Init
    {
        public static void Initalize()
        {
            OLDHAL.Bootstrap.Init();
            OLDHAL.PCI.Setup();
        }
    }

    public class System
    {
        public static void CrashSys()
        {
            OLDHAL.Power.ACPIReboot();
        }
        public static void ACPIShutdown()
        {
            OLDHAL.Power.ACPIShutdown();
        }

        public static void ACPIReset()
        {
            OLDHAL.Power.CPUReboot();
        }
    }
}