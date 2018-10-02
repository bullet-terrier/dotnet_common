using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fraction_Framework_;


/// <summary>
/// Need to set up another mechanism to handle the exceptions that shoulod be defined here.
/// </summary>
namespace fuzzy_smasher.fuzzy_algorithms
{
    public class needleman_wunsch
    {

        /*
         * Needleman-Wunsh starts off with a matrix. values a gap/mismatch and a match as a value.
         * Need to assign the scoring mechanism for each string.
         */
        public double nw_algorithm(byte[] string_1, byte[] string_2)
        {
            List<Tuple<Tuple<int,int>,Tuple<int, int>>> source = new List<Tuple<Tuple<int, int>, Tuple<int,int>>>();
            // let's align it such that string_1 is the shorter string.

            // that ought to handle it.
            if(string_1.Length > string_2.Length)
            {
                byte[] buffer = string_1;
                string_1 = string_2;
                string_2 = buffer;
            }

            List<double[]> nw_grid = new List<double[]>();
            double nw_value = 0;
            for (int i = 0; i <= string_2.Length; i++)
            {
                nw_grid.Add(new double[string_1.Length + 1]); // initialize the matrix.
            }

            // initialized the first row and column.

            // need to determine what scoring algorithm to use...
            // better yet - how to handle it.
            #region string_scoring

            // score alignment should try to be zero - space/mismatch = -1, match = +1;
            // using an alignment +-1 to work through it.
            // optimal alignment would be the maximum value in the value.
            // max == string_1.length
            // min == -string_1.length
            //

            byte[] s1_align = new byte[string_1.Length + 1];
            byte[] s2_align = new byte[string_2.Length + 1];
            
            // use a negative value from each match. 
            // here's what I'm going to do - populate 0,-1,-2 etc horizontally
            //                               populate 0,-1,-2 etc vertically.


            /*
             *  #################################
             *  #  0 -1 -2 -3 -4 -5 -6 -7 -8 -9
             *  # -1
             *  # -2
             *  # -3
             *  # -4
             *  # -5
             *  # -6
             *  # -7
             *  # -8
             *  # -9
             *  # 
             *  
             */

            // initial grid has been populated here.
            for(int i=0;i<nw_grid[0].Length;i++)
            {
                nw_grid[0][i] = -i;
            }
            for(int i = 0; i< string_2.Length + 1; i++)
            {
                nw_grid[i][0] = -i;
            }

            #endregion

            /*
             * 
             * now-  to traverse the set and build the data.
             * 
             * top or left = indel(insert/delete)
             * 
             */

            for(int i = 1; i<string_2.Length+1;i++)
            {
                byte comp_2 = string_2[i - 1]; // vertical check
                for(int j=1; j<nw_grid[i].Length;j++)
                {
                    int local = -1; // we'll assume that it is false.
                    // check values = 
                    byte comp_1 = string_1[j - 1]; // horizontal check
                    if (comp_1 == comp_2)
                    {
                        local = 1;
                    }
                    double a = nw_grid[i - 1][j]+local;
                    double b = nw_grid[i][j - 1]+local;
                    double c = nw_grid[i - 1][j - 1]+local;
                    //nw_grid[i][j] = new List<double> {  }.Max();
                    nw_grid[i][j] = new List<double> { a, b, c }.Max();
                // set nw_grid[i][j] = ...
                }
            }
            nw_value = nw_grid[string_1.Length][string_2.Length];

            // I'll now need to do comparison on the nw_value to generate a scalar representation of the two entities.

            return nw_value;
        }

        // right - this is where I was running into some issues.
        public double calculate_distance(byte[] string_1, byte[] string_2)
        {
            throw new NotImplementedException("Deprecating this method temporarily - use a measure based on the algorithm for all purposes.");
            double value = 0;

            int max_size = new List<int> { string_1.Length, string_2.Length }.Max();
            int min_size = -max_size;
            int max_scale = 2 * max_size;

            double cur_val = nw_algorithm(string_1, string_2);
            Fraction m = new Fraction(100, max_scale); // this should return 1 percent of max size.
            //cur_val = cur_val + max_size; // scale up to a zero based scale.
            //cur_val = cur_val * (double)m.ToDecimal();




            // I could +/- 50% based on sign...
            return value;
        }

    }
}
