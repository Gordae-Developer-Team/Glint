using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Glint
{
    public class Commands
    {
        public static void ls()
        {
            string fst = Kernel.fs.GetFileSystemType("0:/");
            var dir = Kernel.fs.GetDirectoryListing(Kernel.path);
            long avs = Kernel.fs.GetAvailableFreeSpace("0:/");
            foreach (var directoryEntry in dir)
            {
                if(directoryEntry.mName.EndsWith(".gse"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(directoryEntry.mName);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if(directoryEntry.mEntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(directoryEntry.mName);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(directoryEntry.mName);
                }
            }
            Console.WriteLine(avs + " Bytes Free, File System: " + fst);
        }

        public static void dump(string[] arg)
        {
            try
            {
                var file = Kernel.fs.GetFile(Kernel.path + "" + arg[1]);
                var stream = file.GetFileStream();
                if (stream.CanRead)
                {
                    byte[] text_to_read = new byte[stream.Length];
                    stream.Read(text_to_read, 0, (int)stream.Length);
                    Console.WriteLine(Encoding.Default.GetString(text_to_read));
                }
            } catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        public static void decimal_dump(string[] arg)
        {
            var file = Kernel.fs.GetFile(Kernel.path + "" + arg[1]);
            var stream = file.GetFileStream();

            if (stream.CanRead)
            {
                byte[] text_to_read = new byte[stream.Length];
                stream.Read(text_to_read, 0, (int)stream.Length);
                string normtext = Encoding.Default.GetString(text_to_read);
                byte[] hex = Encoding.ASCII.GetBytes(normtext);
                for (int loop = 0; loop < hex.Length; loop++)
                {
                    Console.Write(hex[loop] + " ");
                }
                Console.WriteLine("\n\n" + hex.Length + " Bytes.");
            }
        }

        public static void cd(string[] arg)
        {
            Kernel.path = Kernel.path + arg[1] + @"\";
        }

        public static void mkdir(string[] arg)
        {
            try
            {
                Kernel.fs.CreateDirectory(Kernel.path + arg[1]);
            } catch
            {
                Console.WriteLine("Error!");
            }
        }

        public static void deldir(string[] arg)
        {
            Kernel.fs.DeleteDirectory(Kernel.fs.GetDirectory(Kernel.path + arg[1]));
        }

        public static void mk(string[] arg)
        {
            Kernel.fs.CreateFile(Kernel.path + arg[1]);
        }

        public static void delfile(string[] arg)
        {
            Kernel.fs.DeleteFile(Kernel.fs.GetFile(Kernel.path + arg[1]));
        }

        public static void clear()
        {
            Console.Clear();
            Shell.Taskbar.redraw();
        }

        public static void about()
        {
            Console.WriteLine("Glint Version " + Kernel.version + ", Kernel Version " + Kernel.kv + ".");
            Console.WriteLine("Gict Version 1.0");
        }

        public static void matrix()
        {
            Demos.matrix.matrixdemo();
        }

        public static void edit(string[] arg)
        {
            try
            {
                if (arg[1] != null || arg[1] != "")
                {
                    Console.WriteLine("You Can Not Give MIV A Argument.");
                }
                else
                {
                    MIV.MIV.StartMIV();
                }
            } catch
            {
                MIV.MIV.StartMIV();
            }
        }

        public static void run(string[] arg)
        {
            Gict.Runtime.Run(Glint.Kernel.path + arg[1]);
        }

        public static void help()
        {
            Console.WriteLine("Help:");
            Console.WriteLine("ls - List Directories.");
            Console.WriteLine("dump {file} - Dump A File.");
            Console.WriteLine("decimal {file} - Same As Dump, But In Decimal.");
            Console.WriteLine("cd {dir} - Goto To A Directory.");
            Console.WriteLine("mkdir {dir} - Create A Directory.");
            Console.WriteLine("deldir {dir} - Delete A Directory.");
            Console.WriteLine("mkfile {file} - Create A File.");
            Console.WriteLine("delfile {file} - Delete A File.");
            Console.WriteLine("clear - Clear The Screen.");
            Console.WriteLine("about - About The Operating System.");
            Console.WriteLine("miv - Start The MIV Text Editor.");
            Console.WriteLine("run {script} - Run A Glint Script File (.gse).");
            Console.WriteLine("gict {code} - Run A Glint Script From The Console.");
        }
    }

    public class Handler
    {
        public static void handle()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(Kernel.path);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(">>>");
            Console.ForegroundColor = ConsoleColor.White;
            var command = Console.ReadLine();

            if (command.Contains("ls")) { Commands.ls(); }

            else if (command.Contains("dump"))
            {
                string[] arg = command.Split(" ");
                Commands.dump(arg);
            }

            else if (command.Contains("decimal"))
            {
                string[] arg = command.Split(" ");
                Commands.decimal_dump(arg);
            }

            else if (command.Contains("cd"))
            {
                string[] arg = command.Split(" ");
                Commands.cd(arg);
            }

            else if (command.Contains("mkdir"))
            {
                string[] arg = command.Split(" ");
                Commands.mkdir(arg);
            }

            else if (command.Contains("deldir"))
            {
                string[] arg = command.Split(" ");
                Commands.deldir(arg);
            }

            else if (command.Contains("mkfile"))
            {
                string[] arg = command.Split(" ");
                Commands.mk(arg);
            }

            else if (command == "matrixdemo")
            {
                Commands.matrix();
            }

            else if (command.Contains("delfile"))
            {
                string[] arg = command.Split(" ");
                Commands.delfile(arg);
            }

            else if (command.Contains("run"))
            {
                string[] arg = command.Split(" ");
                Commands.run(arg);
            }

            else if (command.Contains("miv"))
            {
                string[] arg = command.Split(" ");
                Commands.edit(arg);
            }

            else if (command == "clear") { Commands.clear(); }

            else if (command == "shutdown") { HAL.System.ACPIShutdown(); }
            else if (command == "reboot") { HAL.System.ACPIReset(); }
            else if (command == "about") { Commands.about(); }
            else if (command == "help") { Commands.help(); }
            else if (command.StartsWith("gict "))
            {
                string instructions = command.Substring(5);
                Gict.Runtime.Interpreter(instructions);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("'" + command + "' Is Not A Valid Command ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}