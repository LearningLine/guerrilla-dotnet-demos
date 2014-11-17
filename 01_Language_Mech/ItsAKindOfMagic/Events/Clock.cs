using System;
using System.Threading;

namespace Events
{
    public class Clock
    {
        public event EventHandler<EventArgs> Tick;

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);

                if (Tick != null)
                {
                    Tick(this,EventArgs.Empty);
                }
            }
        }
    }
}