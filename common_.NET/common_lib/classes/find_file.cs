using System;
using System.IO; 
using System.Collections.Generic;

namespace common_lib.classes
{
    public static class find_file
    {
        static public Tuple<object,object> find_most_recent_file(string search_path, string pattern = "")
        {
            //throw new NotImplementedException();
            List<Tuple<object, object>> path_times = new List<Tuple<object, object>>();
            foreach(string a in Directory.EnumerateFiles(search_path))
            {
                if( a.Contains(pattern))
                {
                    path_times.Add(new Tuple<object, object>(a, Directory.GetLastWriteTimeUtc(a)));
                }
            }
            var n = new tuple_comparisons.tuple_set();
            n.set = path_times;
            return tuple_comparisons.max_tup(n);
        }

        static public Tuple<object, object> find_least_recent_file(string search_path, string pattern = "")
        {
            //throw new NotImplementedException();
            List<Tuple<object, object>> path_times = new List<Tuple<object, object>>();
            foreach (string a in Directory.EnumerateFiles(search_path))
            {
                if (a.Contains(pattern))
                {
                    path_times.Add(new Tuple<object, object>(a, Directory.GetLastWriteTimeUtc(a)));
                }
            }
            var n = new tuple_comparisons.tuple_set();
            n.set = path_times;
            return tuple_comparisons.min_tup(n);
        }
    }
}
