using System;
using System.Threading;

namespace Events
{
    public class TaskTicker
    {
        private readonly int tickerId;

        public TaskTicker(int tickerId)
        {
            this.tickerId = tickerId;
        }

        public void Run(Clock clock)
        {
            const int nSubscriptions = 500;

            for (int i = 0; i < nSubscriptions; i++)
            {
                clock.Tick += PrintTaskId;
            }

            for (int i = 0; i < nSubscriptions; i++)
            {
                clock.Tick -= PrintTaskId;
            }
            Console.WriteLine("All Done");
        }

        private void PrintTaskId(object sender, EventArgs e)
        {
            Console.Write("{0} ",tickerId);
        }
    }
}