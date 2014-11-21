using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCBehavior
{
    public class GCMe
    {
        private int data;
        private long _;
        private long __;
        private long ____;
        private long _____;
        private long _______;
        private long ________;
        private long _________;
        private long ___________;
        private long ____________;
        private long _____________;
        private long ______________;


        public GCMe()
        {
            unsafe
            {
                fixed (int* ptr = &data)
                {
                    Console.WriteLine((int) ptr);
                }
            }
        }
    }

    public class GCMe2 : IDisposable
    {
        ~GCMe2() // C++ Destructor..But not the same
        {
            // Close XML file, NOT
        }

        public void Dispose()
        {
            // Clean up any unmanaged resources
            GC.SuppressFinalize(this);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public Person HoldsOnTo { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, HoldsOnTo: {1}", Name, HoldsOnTo);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // MarkAndCompact();

          //  Generations();

         //   FinalizersSuck();

            Person rich = new Person() {Name = "Rich"};

            GC.Collect();
            GC.Collect();

            Console.WriteLine(GC.GetGeneration(rich));

            Person youngSexyThing = new Person() {Name = "Sexy Kitten"};
          
            rich.HoldsOnTo = youngSexyThing;

            var youngSexyThingWeakRef = new WeakReference(youngSexyThing);
            youngSexyThing = null;



            Console.WriteLine(rich);

           // Console.WriteLine("Young sexy thing is in {0}",GC.GetGeneration(youngSexyThing));

            rich.HoldsOnTo = null;
            rich = null;

            // No Live roots to Young Sexy Thing

            GC.Collect(0); // Perform a GC on generation zero objects only

            // Is Young sexy thing alive ????? Tune in next week

            Console.WriteLine("Is young sexy thing alive {0}",
                youngSexyThingWeakRef.IsAlive ? "Yes":"No");

            GC.Collect(1);

            Console.WriteLine("Is young sexy thing alive {0}",
            youngSexyThingWeakRef.IsAlive ? "Yes" : "No");

            GC.Collect(2);

            Console.WriteLine("Is young sexy thing alive {0}",
            youngSexyThingWeakRef.IsAlive ? "Yes" : "No");

        }

        private static void FinalizersSuck()
        {
            int counter = 0;
            while (2*2 != 5)
            {
                new GCMe2().Dispose();
                counter++;
                if (counter%20000000 == 0) Console.Write(".");
            }
        }

        private static void Generations()
        {
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            Random rnd = new Random();
            object[] history = new object[1000000];

            while (true)
            {
                int pos = rnd.Next(history.Length);
                history[pos] = new object();
            }
        }

        private static void MarkAndCompact()
        {
            for (int i = 0; i < 32000; i++)
            {
                new GCMe();
            }
        }
    }
}
