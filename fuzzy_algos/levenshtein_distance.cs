using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuzzy_smasher.fuzzy_algorithms
{
    public class levenshtein_distance
    {

        /*
         * So this works for the most part - the last step indexing is a little bit off,
         * but we should be able to fix that soon enough.
         * Lastly - I'm expressing these algorithms in a strongly typed language to allow
         * us to recreate the algorithm in as many ways as possible.
         * 
         * Double check the difference.
         * 
         * Should handle strings of different length
         * 
         * 
         * IF THE STRING IS OF LENGTH ZERO WE GET ERRORS.
         * 
         * 
         */
        // That's why - j/k extend beyond the values.
        static public double calculate_distance(byte[] string_1, byte[] string_2)
        {
            double levenshtein_distance = 0;

            int m = string_1.Length;
            int n = string_2.Length;

            if(m==0 || n==0)
            {
                // use this to return the maximum distance that would likely cause issues.
                return (double)(new List<int> { m, n }.Max() - new List<int> { m, n }.Min());
            }

            List<double[]> mn = new List<double[]>();
            for (int i = 0; i <= m; i++)
            {
                mn.Add(new double[n + 1]); // add new empty values.
            }
            foreach (double[] a in mn)
            {
                a[0] = mn.IndexOf(a);
                //Console.WriteLine($"M:{mn.IndexOf(a)} N:{a[0]}");
            }
            for (int i = 0; i < mn[0].Length; i++)
            {
                mn[0][i] = i;
                //Console.WriteLine($"M:{0} N:{mn[0][i]}");
            }
            //Console.WriteLine("We've Made IT!");
            //foreach ( a in mn)
            for (int a = 1; a < m; a++) // ALRIGHT - THIS RETURNS A LEVENSHTEIN DISTANCE.
            {
                //Console.WriteLine($"Current row: {a}");
                //Using the Fraction class here.
                int j = a;
                for (int i = 1; i < mn[a].Length; i++)
                {
                    //Console.WriteLine($"Current Column: {i}");
                    int cost = 0;
                    if (string_1[j - 1] != string_2[i - 1]) //
                    {
                        cost = 1;
                    }
                    // I'll need to determine the minimum from this set.
                    // basically for each value [i,j] = min[x,y,z] //
                    // Cell directly above
                    double x = mn[j - 1][i] + 1;
                    // Cell directly left
                    double y = mn[j][i - 1] + 1;
                    // cell above and left.
                    double z = mn[j - 1][i - 1] + cost;
                    List<double> chk = new List<double> { x, y, z };
                    //Console.WriteLine($"VALUE: {chk.Min()}");
                    mn[j][i] = chk.Min();
                }
            }
            levenshtein_distance = mn[m - 1][n - 1]; // should return the value. We'll give it a shot.

            return levenshtein_distance;
        }

        // levenshtein_similarity? Might be useful.
    }
}
