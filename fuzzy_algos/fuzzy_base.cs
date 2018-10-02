using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuzzy_smasher.fuzzy_algorithms
{
    /*
     * just using this to store the information that needs to be 
     * shared by all of the algorithms.
     * 
     * Never mind - since I'm using the other algorithms as static classes
     * I cant inherit from fuzzy algorithms directly at least.
     */
    static class fuzzy_base: object
    {
        static public byte[] ToBytes(string string_1)
        {
            char[] n = new char[string_1.Length];
            for (int i = 0; i < string_1.Length; i++)
            {
                n[i] = (char)string_1[i];
            }
            byte[] m = new byte[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                m[i] = (byte)n[i];
            }
            return m;
        }
    }
}
