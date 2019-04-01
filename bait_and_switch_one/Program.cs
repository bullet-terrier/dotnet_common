using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bait_and_switch_one
{
    class Program
    {
        // This would actually be a decent way to describe a series of states for a game...
        static void Main(string[] args)
        {
            string ags = "";
            int ctr = 0;
            switch (ags)
            {
                case "":
                    ctr++;
                    Console.WriteLine($"{ctr}");
                    goto default;
                    break;
                default:
                    ctr++;
                    Console.WriteLine($"{ctr}");
                    if (ctr > 10000) { break; }
                    goto case "";
                    break;
            }

        }
    }
}
