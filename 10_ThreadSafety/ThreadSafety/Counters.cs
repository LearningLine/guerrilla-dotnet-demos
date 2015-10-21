using System.Linq;
using System.Net;
using System.Threading;

namespace ThreadSafety
{
    internal class Counter : ICounter
    {
        private int value;

        public int Value
        {
            get { return value; }
        }

        public void Increment()
        {
            value++;
        }
    }
}