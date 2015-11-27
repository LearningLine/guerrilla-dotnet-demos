using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParallelUtil
{
    [global::System.Serializable]
    public class ParallelUtilException : Exception
    {
        public ParallelUtilException() { }
        public ParallelUtilException(string message) : base(message) { }
        public ParallelUtilException(string message, Exception inner) : base(message, inner) { }
        protected ParallelUtilException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
