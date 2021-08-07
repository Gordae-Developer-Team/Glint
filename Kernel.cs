using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Glint
{
    public class Kernel : Sys.Kernel
    {
        // Critical Variables
        public static string version = "3.0 Beta Testing Release";
        public static string kv = "3.0.0";
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static string path = @"0:\";
        public static string timestr = "20" + HAL.RTC.Year + "/" + HAL.RTC.Month + "/" + HAL.RTC.Day;
        public static List<string> processes = new List<string>();
        public static string task = "                                                        Type 'help' For Commands";
        public static bool root;

        protected override void BeforeRun() 
        {
            try
            {
                Console.WriteLine("Glint Kernel Revision " + kv + ", Now Trying To Boot...");
                root = true;
                Console.WriteLine("Starting Bootstrap And PCI Drivers...");
                HAL.Init.Initalize();
                Console.WriteLine("Starting File System...");
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                Console.WriteLine("Successfully Booted! Now Trying To Load CLI...");
                Console.Clear();
                processes.Add("shell");
                Shell.Taskbar.draw();
                root = false;
            } catch(Exception e)
            {
                Console.CursorVisible = false;
                Console.WriteLine("panic('" + e.Message + "')");
                Console.WriteLine("Kernel Version: " + kv);
                Console.WriteLine("Root Mode: " + Kernel.root);
                Console.ReadKey();
                HAL.System.ACPIReset();
            }
        }
        protected override void Run()
        {
            try
            {
                Shell.Taskbar.draw();
                Handler.handle();
            }
            catch (Exception e)
            {
                Console.CursorVisible = false;
                Console.WriteLine("panic('" + e.Message + "')");
                Console.WriteLine("Kernel Version: " + kv);
                Console.WriteLine("Root Mode: " + Kernel.root);
                Console.ReadKey(); 
                HAL.System.ACPIReset();
            }
        }
    }
}