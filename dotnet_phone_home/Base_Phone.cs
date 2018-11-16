
#define ASSUME_DENY
#define ASSUME_ALLOW
#define ASSUME_SELF_DESTRUCT
#define HARDCODED_URL
#define GITHUB_STATUS_API
#define COMVISIBLE
namespace phone_home_dotnet
{

    // might have a better time using partial class, with a handful of distributed mechanisms.
    // usage - put this in the beginning of your main loop to ping a web resource to determine if the
    // process needs to continue executing.
    // you can check it repeatedly by instantiating another instance of the class.
    // since I'm in debug mode - I should see this always exit the environment.
    // main things being that we'll be forcing an application to quit. I'll try on a debug copy of one of my 
    // other tools.
    using System;
#if COMVISIBLE
    using System.Runtime.InteropServices;
    [ComVisible(true)]
#endif 
    public class Base_Phone : Phone_Home_Interface
    {
#if HARDCODED_URL
        // alright - storing this as plaintext - we'll see if it's easy to handle.
        const string HOME_URL = "https://bullet-terrier.github.io/phone_home/";
#endif
#if GITHUB_STATUS_API
        // *might* conditionally block some of the downstream effects if certain global (really) events occur.
#endif
        // these are members that should always exist.
#region CONSTANT INTERFACE MEMBERS
        public DateTime PERIOD_END { get; }
        public DateTime PERIOD_START { get; }
        public bool ALLOW_GRACE { get; }
        public bool ALLOW_EXEC { get; }
        public string DEST_URL { get; } // constructor should set this to reference the hardcoded url.
#endregion

#if DEBUG
        public void KILL_EVERYBODY() { }
        public bool PHONE_HOME(string URL) { return false; } // this is a test mode.
        public void TRIAL_MODE() { }
        public void TERMINATE() { }
        protected bool parse_response_bytes(byte[] buffer = null)
        {
            // default to false, becuase I want to test what this does when it doesn't find home.
            return false;
        }
#else
        protected bool parse_response_bytes(byte[] buffer)
        {
            // todo: complete this method.
            // inject logic to handle the output here.
            return true;
        }
#if ASSUME_DENY
        // Terminate method to exit program gracefully and handle any hooked methods.
        public void TERMINATE()
        {

        }
#endif
#if ASSUME_ALLOW
        // Allow limited execution of the process that this is guarding.
        public void TRIAL_MODE()
        {

        }
#endif
#if ASSUME_SELF_DESTRUCT
        // Destroy things - someone truly insane would use this.
        [Obsolete("THIS METHOD IS DANGEROUS - DON'T IMPLEMENT THIS UNLESS YOU ARE SURE OF THE CONSEQUENCES")]
        public void KILL_EVERYBODY()
        {
            Environment.Exit(2);
        }
#endif
        // method to determine the output - we'll have to be more specific about what we want it to do.
        public bool PHONE_HOME(string URL)
        {
            // 
            return false;
        }
#endif
        // always use these - since they're part of the main mechanism.

#region CONSTRUCTOR


        // alright - so if we inherit this class - we can set up some overrides - so I'll add those in 
        // passing functions as arguments - how would I go about that - unless I just attached it to an object.
        public Base_Phone( bool default_allow = false  )
        {
            // so the default will be the following:
            this.DEST_URL = HOME_URL;
            this.ALLOW_GRACE = false;
            this.PERIOD_START = DateTime.Today;
            // no grace periods - they can attempt to relaunch it.
            // if they go through the trouble of duplicating the endpoint, then I'm not going to worry too much.
            // using the html methods.
            try
            {
                // improve the entirety of this logical block.
                // could change this to only look at the headers, it would be faster,
                // and more maintainable.
#if DEBUG
                var rq = System.Net.HttpWebRequest.Create("https://status.github.com/messages");
                var rp = rq.GetResponse();
                var st = rp.GetResponseStream();
                byte[] buff = new byte[st.Length];
                // I could make a more efficient version of this.
                st.Read(buff, 0, (int)st.Length);
                //st.Read(buff, 0, (int)rp.ContentLength);

                System.IO.File.WriteAllBytes("./response_bytes.txt", buff);
#else
                // to be replaced with phone_home()
                
                // this does get the response, but it doesn't interpret the response.
                // I"ll hardcode the value that it should expect I suppose.
                var rq = System.Net.HttpWebRequest.Create("DEST_URL");
                var rp = rq.GetResponse();
                var st = rp.GetResponseStream();
                byte[] buff = new byte[st.Length];
                // I could make a more efficient version of this.
                st.Read(buff, 0, (int)st.Length);
                //st.Read(buff, 0, (int)rp.ContentLength);
#endif
                // if the response contains some bytes, do something.
                this.ALLOW_EXEC = parse_response_bytes(buff);
                if (!this.ALLOW_EXEC) { throw new Exception(); } // simply hit the main exit loop- it'll look like a standard abend.
            }
            catch
            {
                // if exception is encountered - we'll set it to toggle the truth state.
                /*if(this.ALLOW_GRACE && )
                {
                    this.PERIOD_END = this.PERIOD_START.AddDays(1);
                }
                I'm not shipping this with a grace period. if it's down, it's down.
                 */
                // Also not allowing an execute - so we'll just drop off the face of the earth with a no-start error.
                /*
               if(this.ALLOW_EXEC)
               {

               }
               */
               // I'll be nice and at least show that there was an abend.
                Environment.Exit(1); 
            }

        }


#endregion

    }
}
