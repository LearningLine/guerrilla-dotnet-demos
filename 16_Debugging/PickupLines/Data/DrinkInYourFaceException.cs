using System;

namespace PickupLines.Data
{
    [Serializable]
    public class DrinkInYourFaceException : Exception
    {
        public DrinkInYourFaceException() { }
        public DrinkInYourFaceException(string message) : base(message) { }
        public DrinkInYourFaceException(string message, Exception inner) : base(message, inner) { }
        protected DrinkInYourFaceException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}