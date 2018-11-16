/*
 *  This is to provide access to the necessary phone home objects necessary in the
 *  use of lightweight DRM utilities.
 */
using System;
using System.Collections.Generic;
// if not 4.1 and greater, requests will always fail - so you've been warned.
$if$ ($targetframeworkversion$ >= 4.0)using System.Net;$endif$
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;

namespace $rootnamespace$
{
	class $safeitemrootname$ : phone_home_dotnet.Phone_Home_Interface
	{
       bool allow { get; }
       public void KILL_EVERYBODY() { Environment.Exit(2); }
       public void TRIAL_MODE() { Environment.Exit(1); }
       public void TERMINATE() { Environment.Exit(0); }
       
       public bool PHONE_HOME(string P)
       {

           var rq = HttpWebRequest.Create(P);
           var rp = rq.GetResponse();
           var rsp = rp.GetResponseStream();
           
           for (int i = 0; i < )

       }
	}
}

