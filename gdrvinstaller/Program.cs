using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace gdrvinstaller
{
    internal class Program
    {
        public static void ExtractFiles(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //This is Very Important Code... DON'T CHANGE THIS!!! 

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        static void Main(string[] args)
        {
            string gdrv_dir = "C:\\GDRV_Driver\\gdrv.sys";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Welcome to the GDRV Installer(Gigabyte Vulnerable Driver)... So Enjoy to use this!!!");
            Directory.CreateDirectory("C:\\GDRV_Driver");
            ExtractFiles("gdrvinstaller", "C:\\GDRV_Driver", "Resources", "gdrv.sys");
            if (!File.Exists("C:\\GDRV_Driver\\gdrv.sys"))
            {
                Console.WriteLine("This File is Not Exists... Or Not Extracted!!!");
            }
            else
            {
                ProcessStartInfo info_s = new ProcessStartInfo("cmd.exe");
                info_s.Verb = "runas";
                info_s.CreateNoWindow = false;
                info_s.Arguments = $@"/k sc create gdrv binpath={gdrv_dir} type=kernel";
                Process.Start(info_s);
                Environment.Exit(3112);
            }
        }
    }
}
