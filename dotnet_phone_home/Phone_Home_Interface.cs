
#if DEBUG
#define COMVISIBLE
#define ASSUME_DENY
#define ASSUME_ALLOW
#define ASSUME_SELF_DESTRUCT
#endif

using System;

// THESE ARE PRIMARILY FOR THE IMPLEMENTATION OF THE OBJECT TO USE.
// compiles with one of three symbols - 
// #ASSUME_DENY --> assume the program needs to stop if this doesn't see "home"
// #ASSUME_ALLOW --> assume the program will work if it doesn't see "home", but seeing it would cause it to pause.
// #ALLOW_OFFLINE --> will provide a few additional methods that allow the function to run if it gets severed.
// **BONUS** #ASSUME_SELF_DESTRUCT --> halt and catch fire friends.
// #ELSE
namespace phone_home_dotnet
{

    #if COMVISIBLE
    using System.Runtime.InteropServices;
    [ComVisible(true)]
    #endif
    interface Phone_Home_Interface
    {
        string DEST_URL { get; } // required regardless.
        bool ALLOW_EXEC { get; } // will be determined by the process.
        bool ALLOW_GRACE { get; } // each class can allow for a grace period to be allowed.
        DateTime PERIOD_START { get; } // DateTime that will need to be stored.
        DateTime PERIOD_END { get; }  // expiration period end.


        bool PHONE_HOME(string url); // this is just a suggestion really.
        

#if ASSUME_DENY
        void TERMINATE();
#endif
#if ASSUME_ALLOW
        void TRIAL_MODE();
#endif
#if ASSUME_SELF_DESTRUCT
        void KILL_EVERYBODY();
#endif

    }
}
