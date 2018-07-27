using System;
using System.Net.Http;
//using System.Web;

/// <summary>
/// Writing a mini-package to help ping healthchecks from more... restrictive environments.
/// </summary>
namespace dotnet_webget_lite
{
    class Program
    {
        // setting up an alternative to the healthcheck ping -.
        static void Main(string[] args)
        {
            int returncode = 0;
            // Console.WriteLine("Hello World!");
            foreach (string a in args)
            {
                Console.WriteLine(a);
                try
                {
                    HttpClient htp = new HttpClient();
                    // htp.BaseAddress = a; // attempt to set the addy to a;
                    var m = htp.GetAsync(a);
                    Console.WriteLine(m.Result);
                    Console.WriteLine(m.ToString());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    returncode = 1;
                }
            }
            Console.WriteLine(returncode);
        }
    }
}