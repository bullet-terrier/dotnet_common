using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace common_lib.classes
{
    /// <summary>
    /// .NET implementation of my Python "tuple_comparisons.py" module.
    /// This is used extensively in some file comparison tools.
    /// </summary>
    public class tuple_comparisons
    {
        public class tuple_set
        {
            // storing type of object, since I don't know necessarily what we'll be using.
            // we'll set this to private, and we'll add accessor methods to the class
            // just leaving these alone.
            public List<Tuple<object,object>> set { get; set; }

            public tuple_set()
            {

            }
        }
        
        /// <summary>
        /// 
        /// NOTE! this might throw an error if tuple_set.set has a length of 0;
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <returns>
        ///    Tuple of two tuples, organized by their value.
        /// </returns>
        static public Tuple<Tuple<object,object>,Tuple<object,object>> min_max_tup(tuple_set set, int item = 0)
        {
            //Tuple<Tuple<object,object>>
            int mindex = 0;
            int maxdex = 0;
            // maybe I could convert the a.ItemX into an integer representation?
            float t_min = 0;
            float t_max = 0;

            // Do I want to have two branches, or one complicated loop.
            foreach(Tuple<object,object> a in set.set)
            {
                // casting as an integer - maybe I'll try floats?
                if((float)a.Item2 > t_max)
                {
                    maxdex = set.set.IndexOf(a);
                    t_max = (float)a.Item2;
                }
                if((float)a.Item2 < t_min)
                {
                    mindex = set.set.IndexOf(a);
                    t_min = (float)a.Item2;
                }
            }
            return new Tuple<Tuple<object, object>, Tuple<object, object>>(set.set[mindex], set.set[maxdex]);
        }

        static public Tuple<object,object> min_tup(tuple_set set, int item = 0)
        {
            return min_max_tup(set).Item1;
        }

        static public Tuple<object,object> max_tup(tuple_set set, int item = 0)
        {
            return min_max_tup(set).Item2;
        }
    }
}
