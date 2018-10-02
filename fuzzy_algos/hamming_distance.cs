using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuzzy_smasher.fuzzy_algorithms
{
    // make these classes static for the sake of the mechanism
    public class hamming_distance
    {
        static private double raw_distance(byte a, byte b)
        {
            double value = 0;
            int m = a ^ b; // I think this was XOR in C# as well.
            while (m > 0)
            {
                value += 1;
                m = m & (m - 1);
            }
            return value;
        }

        static public byte[] ToBytes(string string_1)
        {
            char[] n = new char[string_1.Length];
            for (int i = 0; i < string_1.Length; i++)
            {
                n[i] = (char)string_1[i];
            }
            byte[] m = new byte[n.Length];
            for(int i = 0;i<n.Length;i++)
            {
                m[i] = (byte)n[i];
            }
            return m;
        }

        /*
         *  I've got some error in here - but not super sure what it i.
         */
        static public double calculate_distance(byte[] string_1, byte[] string_2)
        {
            double hamming_distance = 0;
            if (string_1.Length < string_2.Length)
            {
                byte[] buf = string_2;
                string_2 = string_1;
                string_1 = buf; // that was just bad - how did I mess that up.
                // reorder them such that string_1.Length >= string_2.length;
            }
            if (string_1.Length != string_2.Length)
            {
                // alright - reinitialize string_2.Length to match string_1.Length;
                byte[] buf = new byte[string_1.Length];
                for(int i = 0; i<string_2.Length;i++)
                {
                    buf[i] = string_2[i];
                }
                string_2 = buf; // this should fix the length and append '\0' as necessary.
            }
            /*if (string_1.Length > string_2.Length)
             * Alright - that should 
            {
                /*
                 * I think this is causing the issue?
                 * 
                 * Fix it by updating the length?
                 * 
                 * need to make sure that the bytes are the correct length.
                 
                byte[] buf_bytes = new byte[string_1.Length];
                for(int i = 0; i<string_2.Length;i++)
                {
                    buf_bytes[i] = string_2[i]; //
                }
                for(int i = string_2.Length; i< buf_bytes.Length; i++)
                {
                    buf_bytes[i] = 0; // pad the remaining slots with "0"
                }
            }*/
            for(int i = 0; i < string_1.Length;i++)
            {
                hamming_distance += hamming_distance + raw_distance(string_1[i], string_2[i]);
            }
            // should return the raw difference
            return hamming_distance;
        }
    }
}
