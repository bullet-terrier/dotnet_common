using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace common_lib
{
    public class NotImplementedException : Exception { public NotImplementedException() { throw new Exception("Operation or object not implemented"); } }
    public class RecursionException : Exception { public RecursionException() { throw new Exception("Error encountered during recursive error handling"); } }
}
