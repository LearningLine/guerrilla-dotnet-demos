using System;

namespace TheScheduler
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}