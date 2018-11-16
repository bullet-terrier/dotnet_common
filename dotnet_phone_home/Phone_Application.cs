#if DEBUG
#define COMVISIBLE
#endif
using System;

namespace phone_home_dotnet
{
#if COMVISIBLE
using System.Runtime.InteropServices;
[ComVisible(true)]
#endif 
    public class PhoneApplication : Phone_Application_Interface
    {

        public PhoneApplication()
        {
            this.Run();
        }

        // override with a reference to your main loop to lock it down.
        public void Run()
        {
            //
        }
    }
}
