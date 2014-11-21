using System;

namespace Payments
{
    public class RealTimeService : ITimeService
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}