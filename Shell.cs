using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Shell
{
    public class Taskbar
    {
        static string timestr = "20" + HAL.RTC.Year + "/" + HAL.RTC.Month + "/" + HAL.RTC.Day;
        static bool cleared = false;
        static bool drawed = false;
        public static void draw()
        {
            if (Glint.Kernel.processes.Contains("shell") == true)
            {
                if(drawed == false)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(Glint.Kernel.task);
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(timestr + "\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    drawed = true;
                }
            }
            if (Glint.Kernel.processes.Contains("shell") == false)
            { 
                if(cleared == false)
                {
                    Console.Clear();
                    cleared = true;
                }
            }
        }

        public static void redraw()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Glint.Kernel.task);
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(timestr + "\n");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}