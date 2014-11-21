using System;
using System.Runtime.Serialization;

namespace Payments
{
    [Serializable]
    public class CardDecodingException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CardDecodingException()
        {
        }

        public CardDecodingException(string message)
            : base(message)
        {
        }

        public CardDecodingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CardDecodingException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}