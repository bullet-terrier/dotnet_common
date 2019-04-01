using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;


namespace Call_WinSCP
{
    // this module has just been full of modules.
    // not the cleanest solution to parsing arguments, but this should work for now.
    class Program
    {
        public static string hostkey = "";
        public static bool verbose = false;
        public static List<string> pargs = new List<string>(); // positional arguments.
        public static List<string> sargs = new List<string>(); // list of switch args
        public static Dictionary<string, string> kargs = new Dictionary<string, string>(); // list of kwargs
        

        /// <summary>
        /// Parse the url input "sftp://"
        ///                     "ftp://"
        ///                     "scp://"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Protocol parseProtocol(Uri uri)
        {
            //Uri uri = new Uri(url);
            Protocol pcl;
            
            // we can expand here.
            switch (uri.Scheme)
            {
                case ("sftp"):
                    pcl = Protocol.Sftp;
                    break;
                case ("scp"):
                    pcl = Protocol.Scp;
                    break;
                // default is ftp
                default:
                    pcl = Protocol.Ftp;
                    break;
            }
            return pcl; // 
        }

        static string[] user_pass(Uri uri)
        {
            string[] up = new string[2];
            // that was an oversight.
            if(uri.UserInfo!=""&&uri.UserInfo.Contains(":"))
            {
                up[0] = uri.UserInfo.Split(':')[0];
                up[1] = uri.UserInfo.Split(':')[1];
            }
            else
            {
                up[0] = uri.UserInfo;
                up[1] = "";
            }
            
            return up;
        }


        public static TransferMode parseTransfermode(string transfermode)
        {
            TransferMode transfermodett
            switch (transfermode)
            {
                case "binary":
                    break;
                case "ascii":
                    break;
                case "utf-16":
                    break;
                default:
                    // I wonder what would happen if I forced an infinite loop in this.
                    break;
            }

        }


        public static SessionOptions parseOpts(string arg)
        {
            Protocol protocol;
            string hostname;
            string username;
            string password;
            string sshhostkey;
            SessionOptions sopts;

            Uri uri = new Uri(arg); // 

            protocol = parseProtocol(uri);

            string[] up = user_pass(uri);
            username = up[0];
            password = up[1];
            hostname = $"{uri.Host}:{uri.Port}";


            // we need to change this *possibly* to allow more handles.
            if (hostkey != "")
            {
                sshhostkey = hostkey;
                sopts = new SessionOptions()
                {
                    Protocol = protocol,
                    HostName = hostname,
                    UserName = username,
                    Password = password,
                    SshHostKeyFingerprint = sshhostkey
                };
            }
            else
            {
                sopts = new SessionOptions()
                {
                    Protocol = protocol,
                    HostName = hostname,
                    UserName = username,
                    Password = password
                };
            }
            // maybe just construct it with our recently initialized values?


            return sopts; // return the new session options as determined by the arg.
        }

        // find the nearest hostkey option.(might just add a layer on top of this to make it easier.
        // effectively just handling thj



        /// <summary>
        /// Args should contain, in order: the sftp connection information [protocol]/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string uri = null; // you need this at least.
            string remote_directory = null;
            string local_directory = null; // I might dump these down into the keyword arguments, or implicitly as positional arguments.
            List<string> actions = new List<string>(); // this will handle the additional options (put/get) etc.
                                                       // We'll set it to assume that just a host implies a "pull *" operation.

#if DEBUG
            for(int j = 0;j<args.Length;j++)
            {
                Console.WriteLine(args[j]);
            }
#endif
            // generate a dictionary to access input.
            // alrighty - the first few things should simply be the first cases.
            // THAT WAS AN EPIC TYPO. INITIALIZED I as 9 INSTEAD OF 0
            for(int i = 0; i<args.Length;i++)
            {
                if (args[i].Contains("="))
                {
                    string[] kp = args[i].Split('=');
                    kargs.Add(kp[0], kp[1]); // don't allow 
#if DEBUG
                    Console.WriteLine($"{args[i]}: Keyword argument");
#endif 
                }
                // if we're opening with a dash - then load into the list of switches.
                else if (args[i].Substring(0,1) == "-")
                //else if(args[i].Substring(0,3).Contains("-"))
                {
                    sargs.Add(args[i]); //
#if DEBUG
                    Console.WriteLine($"{args[i]}: Switch argument");
#endif 
                }
                // what is causing this to get add... ooooh.....
                else
                {
                    pargs.Add(args[i]);
#if DEBUG
                    Console.WriteLine($"{args[i]}: Positional argument");
#endif 
                }
            }

            // to handle a variable number of positional arguments.
            for(int i = 0;i<pargs.Count;i++)
            {
                switch (i)
                {
                    case 0:
                        uri = pargs[i];
#if DEBUG
                        goto default;
#endif
                        break;
                    case 1:
                        remote_directory = pargs[i];
#if DEBUG
                        goto default;
#endif
                        break;
                    case 2:
                        local_directory = pargs[i];
#if DEBUG
                        goto default;
#endif
                        break;
                    default:
                        if (i > 2) { actions.Add(pargs[i]); }
                        Console.WriteLine($"Unused positional argument - {pargs[i]}");
                        break;
                }

            }
#if DEBUG

            // now during debugging we'll just list the sorted arguments and make sure that key value pairs are fine.
            // we're at a few hundred lines and mostly just dealing with argument parsing.
            foreach(string a in kargs.Keys)
            {
                Console.WriteLine($"{a}:{kargs[a]}");
            }
            foreach(string a in sargs)
            {
                Console.WriteLine($"{a}");
            }
            foreach(string a in pargs)
            {
                Console.WriteLine($"{a}");
            }
            Console.WriteLine($"URL: {uri?? "<unassigned>"}");
            Console.WriteLine($"Remote: {remote_directory ?? "<unassigned>"}");
            Console.WriteLine($"Local: {local_directory ?? "<unassigned>"}");

            //Console.ReadLine();
#endif

            // we'll want to try to trigger the most basic form of the function.


            /*
                        // scan for hostkey - '
                        // we don't need this now..
                        // what would the minimum number of necessary arguments be?
                        // I'm thinking just one should be fine.
                        for(int i = 0;i<args.Length;i++)
                        {
                            if(args[i].Contains("-hostkey"))
                            {
                                hostkey = args[i].Split('=')[1]; // if this isn't formed well then it will break.
                            }
                        }
            */
            // scan for put/pull files.
            //
            string op_ = uri + " " + hostkey;
#if DEBUG
            Console.WriteLine("+Establishing connection based here..."+op_);
#endif
            SessionOptions sopt = parseOpts(op_);
            // now we could try to run it through the session options mechanism.

            try
            {
                // add session data;
                using (Session session = new Session())
                {
                    session.Open(sopt); // funny - I used the same characters as I ended up using in the other application.

                    TransferOptions topts = new TransferOptions(); // not sure what needs to be added in here.
                    TransferOperationResult tor; // just declare it.
                    
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
