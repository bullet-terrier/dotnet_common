using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fraction_Framework_;


namespace fuzzy_smasher.fuzzy_algorithms
{
    public class jaro_winkler_distance
    {
        /*
         *  ADD THE TOBYTE() method to this object.
         */
        // Requires fraction to function - returns a double still.
        public static double jaro_similarity(byte[] string_1, byte[] string_2)
        {

            /*
             * 
             * sim(w) = sim(j) + (lp(1-simj)) allowing
             *   simj is the jaro similarity for strings s1 and s2
             *   l is the length of the common prefix at the start of the string up to a maximum of four characters.
             *   p is a constant scaling factor for how much the score trends to having common prefixes. Should not exceed .25
             *   
             *   The number of "matching" but different sequence order characters divided by 2 defines the number of transpositions.
             *   (CRATE/TRACE) RAE MATCH No Transpositions. ONLY IF THEY ARE WITHIN max(string_1,string_2)/2 -1 would they be 
             *   considered as a transposition.
             *   
             *   -------------------------------------------------------------------------------------------------------------
             *   
             *   input_1; input_2;
             *   
             *   match_buffer[]; // length of at max( input_1, input_2 )
             *   
             *   return 0 if no matching characters.
             *   
             *   this implementation shouldn't be too bad.
             *   
             *   
             *   
             *   -------------------------------------------------------------------------------------------------------------
             */
            // gather some information first
            double similarity_factor = 0; // default to being completely dissimilar
            if (string_1.Length > string_2.Length)
            {
                byte[] buffer = string_2;
                string_2 = string_1;
                string_1 = buffer;
                // swap them so that I only use the shorter string.
            }
            int s1_len = string_1.Length;
            int s2_len = string_2.Length;
            int t_range = (s2_len / 2) - 1; // set the transposition range (s2_len will always be >= max {s1,s2} based on initial logic)
            int s_count = 0; // similarity counter
            int t_count = 0; // transposition counter
            // sim j = 1/3(match/len(s1)+match/len(s2)+match-transposition/m) --> JARO SIMILARITY
            // any values in the longer string beyond the last position wouldn't be matches.
            for (int i = 0; i < s1_len; i++)
            {
                // mark similarities.
                if (string_1[i] == string_2[i])
                {
                    s_count++;
                }
                // check for transpositions if the string contains it.
                else if (string_2.Contains(string_1[i]))
                {
                    int t_min = i - t_range;
                    int t_max = i + t_range;
                    if (t_min < 0) { t_min = 0; }
                    if (t_max > s2_len) { t_max = s2_len; }
                    for (int j = t_min; j < t_max; j++)
                    {
                        if (string_1[i] == string_2[j])
                        {
                            t_count++;
                            break; // if we find a match, don't double count it- we're not measuring how many repeated characters there are.
                        }
                    }
                }
            }
            if (s_count != 0)
            {
                Fraction ft = new Fraction(t_count, 2);
                decimal t = ft.ToDecimal();
                //double t = t_count / 2;
                // trying to cast this as a double to start - may need to convert to decimal.
                // had to add in a new parenthesis - not sure if that's going to mess with my grouping at all.
                similarity_factor = (double)(new Fraction(1, 3).ToDecimal() * (new Fraction(s_count, s1_len).ToDecimal() + (new Fraction(s_count, s2_len).ToDecimal() + (new Fraction((int)(s_count - t), s_count).ToDecimal()))));
            }
            return similarity_factor;
        }

        public static double jaro_winkler_similarity(byte[] string_1, byte[] string_2, int prefix_length = 4, double prefix_scale = 0.1)
        {
            prefix_scale = new List<double> { prefix_scale, 0.25 }.Min();
            double similarity = 0;
            double jaro_similarity_ = jaro_similarity(string_1, string_2);
            if (string_1.Length > string_2.Length)
            {
                byte[] buffer = string_2;
                string_2 = string_1;
                string_1 = buffer;
            }
            if (jaro_similarity_ == 1)
            {
                similarity = 1;
                // no need to add the bonus value since it's a 100% match.   
            }
            else
            {
                int p_match = 0;
                for (int i = 0; i < new List<int> { prefix_length, string_1.Length }.Min(); i++)
                {
                    if (string_1[i] == string_2[i])
                    {
                        p_match++;
                    }
                    else
                    {
                        break;
                    }
                }
                similarity = jaro_similarity_ + (p_match * prefix_scale * (1 - jaro_similarity_));
            }
            return similarity;
        }

        public static double jaro_distance(byte[] string_1, byte[] string_2)
        {
            return 1 - jaro_similarity(string_1, string_2);
        }

        public static double calculate_distance(byte[] string_1, byte[] string_2, int prefix_length = 4, double prefix_scale = 0.1)
        {
            return 1 - jaro_winkler_similarity(string_1, string_2, prefix_length, prefix_scale);
        }
    }
}
