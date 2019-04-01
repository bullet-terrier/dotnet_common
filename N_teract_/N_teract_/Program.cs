using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace N_teract_
{
    static class Program
    {
        //List<>
        // I'll tweak this to make life a little better - 
        // might want to set it up to allow 
        static List<RegistryKey> checkreg(RegistryKey k)
        {
            List<RegistryKey> LRK = new List<RegistryKey>();

            if (k.SubKeyCount == 0)
            {
                LRK.Add(k);
                return LRK;
            }
            for(int i = 0;i<k.SubKeyCount;i++)
            {
                if(k.SubKeyCount>0)
                {
                    LRK.Add(k.OpenSubKey(k.GetSubKeyNames()[i]));
                }
                else
                {
                    foreach(RegistryKey rk in (checkreg(k.OpenSubKey(k.GetSubKeyNames()[i]))))
                    {
                        LRK.Add(rk);
                    }
                }
            }
            return (LRK);
            
        }

        /// <summary>
        /// //The main entry point for the application.
        /// Having some fun here.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        { 
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */


            var y = Registry.Users;
            var z = Registry.LocalMachine;
            var x = Registry.CurrentUser; // beautiful - I can start using this to gather data. Look up currentconfig a bit later.
            var zed = Registry.CurrentConfig;
            //var zed_2 = Registry.PerformanceData;
            List<RegistryKey> myKeys = new List<RegistryKey>();
            // We'll try modifying this one a bit later.

            foreach (var zed_2 in new List<RegistryKey> { Registry.Users, Registry.LocalMachine, Registry.CurrentUser, Registry.CurrentConfig, Registry.PerformanceData, Registry.DynData })
            {
                try
            {
                // this ought to be fun.
                    foreach (string r in zed_2.GetSubKeyNames())
                    {
                        myKeys.AddRange(checkreg(x.OpenSubKey(r)));
                    }
                
            }
            catch(Exception E)
            {
                // suppressing since we're in super secret squirrel mode.
#if DEBUG
                Console.WriteLine(E);
#endif 
            }
                }
            File.WriteAllText("./a", "");
            File.AppendAllText("./a", Environment.UserName);
            File.AppendAllText("./a", "\r\n");
            File.AppendAllText("./a", Environment.UserDomainName);
            File.AppendAllText("./a", "\r\n");
            File.AppendAllText("./a", Environment.SystemDirectory);
            File.AppendAllText("./a", "\r\n");
            File.AppendAllText("./a", Environment.MachineName);
            File.AppendAllText("./a", "\r\n");
            File.AppendAllText("./a", Environment.Is64BitOperatingSystem.ToString());
            File.AppendAllText("./a", "\r\n");
            foreach (RegistryKey rk in myKeys)
            {
                File.AppendAllText("./a",rk.Name+"\r\n");
                foreach (string a in rk.GetValueNames())
                {
                    File.AppendAllText("./a", "\t"+a +":\t"+ rk.GetValue(a)+"\r\n");
                }
                File.AppendAllText("./a", "\r\n");
            }
            // we'll want to recursively traverse some of these nodes.
            
        }
    }
}
