using System;

namespace Payments
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}