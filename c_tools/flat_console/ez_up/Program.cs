#define _WINSCP_
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

//
#if _WINSCP_
using WinSCP;
#else // might need some more tooling to make sure that we can handle a variety of platforms.
#endif

namespace ez_up
{
    static class Program
    {

        static public Dictionary<string, string> configs = new Dictionary<string, string>
        {
            { "map_file","" },
            {"default_protocol","" },
            {"default_host","" },
            {"default_user","" },
            {"default_pass","" },
            {"default_key","" },
            {"default_dest","" }
        }; // I'll need to try to load these values automatically - then load any configurations that may not be in this list.

        public static void refresh_configs()
        {
            foreach(string a in configs.Keys)
            {
                try
                {
                    configs[a]=ConfigurationManager.AppSettings[a];
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Exception encountered: {e}");
                }
            }
        }

        // look for configs not included in this base library.
        public static void additional_configs()
        {
            foreach(string a in ConfigurationManager.AppSettings.Keys)
            {
                if(configs.Keys.Contains(a)){
                    configs[a] = ConfigurationManager.AppSettings[a];
                }
                else{
                    configs.Add(a, ConfigurationManager.AppSettings[a]);
                }
            }
        }

        // this is all we need to use to submit the file... for now...
        public static void ftp_files(out int status, string[] files ,Dictionary<string, object> dict = null)
        {
            string host = "";
            string user = "";
            string pass = "";
            string fing = "";
            var pcol = Protocol.Sftp;

            Dictionary<string, object> lmp = new Dictionary<string, object> {
                { "HostName" , host},
                { "UserName", user },
                {"Password",pass  },
                {"SshHostKeyFingerprint",fing },
                {"Protocol", pcol },
                {"Destination", "" }
            };
            
            // let's map all of the necessary output to something useful...
            foreach(string a in lmp.Keys)
            {
                if(dict.Keys.Contains(a))
                {
                    lmp[a] = dict[a]; //
                }
            }

            WinSCP.SessionOptions opts = new SessionOptions
            {
                Protocol = (Protocol)lmp["Protocol"],
                HostName = (string)lmp["HostName"],
                UserName = (string)lmp["UserName"],
                Password = (string)lmp["Password"],
                SshHostKeyFingerprint = (string)lmp["SshHostKeyFingerprint"]
            };


            // now that we have the options - open connection and send file?
            // this might be a tad slow...
            // 

            int ct = 0;
#if HANDLE_EXCEPCTIONS
            try
            {
#endif
            // make the call to the remote server.
            List<TransferOperationResult> results = new List<TransferOperationResult>();

            using (Session session = new Session())
            {
                session.Open(opts);

                TransferOptions transferOptions = new TransferOptions();
                transferOptions.TransferMode = TransferMode.Binary;
                
                // push the files.
                for(int i = 0;i<files.Length;i++)
                {
                    TransferOperationResult transferResult;
                    
                    // we don't need the additional tools.
                    // this should handle the results.
                    transferResult = session.PutFiles(files[i],(string)lmp["Destination"]);
                    results.Add(transferResult);

                    // so this should catch errors - I might move this to a different block, or handle
                    // it after the rest of the attempted submissions.
                    transferResult.Check();
#if DEBUG
                    foreach(TransferEventArgs transfer in transferResult.Transfers)
                    {
                        Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                    }
#endif
                }
                
            }
#if HANDLE_EXCEPTIONS
            }
            catch(Exception e)
            {
#if DEBUG
            Console.WriteLine($"{e}");
#endif
            }
#endif
                status = ct;
            return;
        }



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //
            // load configuration information.
            //

            // connection default.
            // select connection coming later.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
