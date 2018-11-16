
#if DEBUG
#define COMVISIBLE
#endif
namespace phone_home_dotnet
{
#if COMVISIBLE
    using System.Runtime.InteropServices;
    [ComVisible(true)]
#endif
    interface Phone_Application_Interface
    {
        // should simply contain a run method.
        void Run();
    }
}
