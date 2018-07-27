using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace common_lib.classes
{
    // cleanup module
    public class cleanup
    {
        // using a cleanup object.
        protected List<string> extensions { get; set; }
        protected List<string> paths { get; set; }
        private const string __RECURSION_ERROR__ = "Failed after attempting to rectify the problem recursively. Stopping";

        public void remove_path(object arg)
        {
            paths.Remove((string)arg);
        }

        public void remove_extension(object arg )
        {
            extensions.Remove((string)arg);
        }

        public void register_path(string path, bool retry = false)
        {
            try
            {
                paths.Add(path);
            }
            catch(NullReferenceException NRE)
            {
                this.paths = new List<string>();
                if(retry)
                {
                    throw new Exception(__RECURSION_ERROR__, NRE);
                }
            }
        }

        public void register_extension(string extension, bool retry = false)
        {
            try
            {
                extensions.Add(extension);
            }
            catch(NullReferenceException NRE)
            {
                this.extensions = new List<string>();
                if (retry)
                {
                    throw new Exception(__RECURSION_ERROR__,NRE);
                }
                this.register_extension(extension);
            }
            return;
        }

        /// <summary>
        /// run through the files that are stored in memory - 
        /// </summary>
        public void clean()
        {
            var text_grave = "./text_graveyard{0}";
            Directory.CreateDirectory(text_grave);
            foreach(string path in this.paths)
            {
                try
                {
                    //OperatingSystem x = new OperatingSystem(PlatformID.Win32NT,new Version("10"));
                    foreach(string a in Directory.EnumerateFiles(path))
                    {
                        foreach(string b in this.extensions)
                        {
                            if(a.Contains(b))
                            {
                                Directory.Move(a, text_grave);
                                break;
                            }
                        }
                    }                    
                }
                catch(Exception)
                {
                    throw new Exception("WE DON'T MAKE MISTAKES");
                }
            }
        }

        public cleanup()
        {
            // I'm going to leave this alone.
        }
    }
}
