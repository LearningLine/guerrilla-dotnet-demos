using System;

namespace WeakRefs
{
    class Person
    {
        public Person HoldsOnTo { get; set; } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person oldPerson = new Person();
            GC.Collect();
            GC.Collect();
            Console.WriteLine("Old person in gen {0}", GC.GetGeneration(oldPerson));

            Person lessOldPerson = new Person();
            WeakReference wr = new WeakReference(lessOldPerson);
            oldPerson.HoldsOnTo = lessOldPerson;
            Console.WriteLine("Less old person in gen {0}", GC.GetGeneration(lessOldPerson));
            oldPerson.HoldsOnTo = null;
            GC.Collect(0);

            Console.WriteLine("Less old person is {0}", wr.IsAlive ? "alive" : "gone");
            //Console.WriteLine("Less old person in gen {0}", GC.GetGeneration(lessOldPerson));
        }
    }
}
