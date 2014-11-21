using System;
using System.Runtime.Serialization;

namespace Payments
{
    [Serializable]
    public class CardValidationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public CardValidationException()
        {
        }

        public CardValidationException(string message) : base(message)
        {
        }

        public CardValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CardValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}