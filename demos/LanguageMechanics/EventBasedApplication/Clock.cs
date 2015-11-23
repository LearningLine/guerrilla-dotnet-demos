using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EventBasedApplication
{
    public class Clock
    {
        public event EventHandler<EventArgs> Tick = delegate { };

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
               
               // if (Tick != null)
                {
                    Tick(this, EventArgs.Empty);
                }
            }
        }
    }
}
