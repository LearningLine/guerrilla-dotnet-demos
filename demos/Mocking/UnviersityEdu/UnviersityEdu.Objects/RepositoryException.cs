using System;
using System.Runtime.Serialization;

namespace UnviersityEdu.Objects
{
    [Serializable]
    public class RespositoryException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RespositoryException()
        {
        }

        public RespositoryException(string message) : base(message)
        {
        }

        public RespositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RespositoryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}