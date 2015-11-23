using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.InteropServices;

// Show dynamic in action, then say well how does it work
// perhaps it uses reflection, do that
// Caches the MethodInfo
// Hmmm....reflection invoke obviously still too slow...
// Introduce fast invoker.


namespace Dynamic
{

    public interface IDynamicInvoke<T>
    {
       T Invoke(object target , params object[] args);
    }


    class Program
    {
        static void Main(string[] args)
        {

            //dynamic i = 5;

            //i = "five";

            //Console.WriteLine(i.ToUpper());

            dynamic rich = new ExpandoObject();

            rich.Name = "Rich";

            rich.Age = 50;

            rich.Speak = new Action<string>(s => Console.WriteLine(s));

            Console.WriteLine("{0}, {1}", rich.Name, rich.Age);

            rich.Speak("Hello");













            //var animals = new object[] { new Dog(), new Duck(), new Sheep() };

            //DynamicInvokers(animals);

            //PureDynamic(animals);

            //InterfaceInvokers(animals);
        }

        private static void PureDynamic(object[] animals)
        {
            Stopwatch timer = Stopwatch.StartNew();
            timer.Stop();
            timer = Stopwatch.StartNew();


            for (int i = 0; i < 1000000; i++)
            {
                foreach (dynamic o in animals)
                {
                    o.Nop();
                }
            }

            timer.Stop();
            Console.WriteLine("dynamic took {0}", timer.Elapsed);
        }

        private static void InterfaceInvokers(object[] animals)
        {
            Stopwatch timer = Stopwatch.StartNew();

            for (int i = 0; i < 1000000; i++)
            {
                foreach (INop o in animals)
                {
                    o.Nop();
                }
            }

            timer.Stop();
            Console.WriteLine("Interface took {0}", timer.Elapsed);
        }

        private static void DynamicInvokers(object[] animals)
        {
            var dynamicInvokers = new IDynamicInvoke<object>[]
                                      {
                                        
                                      };

            Stopwatch timer;
            foreach (IDynamicInvoke<object> invoker in dynamicInvokers)
            {
                timer = Stopwatch.StartNew();

                for (int i = 0; i < 1000000; i++)
                {
                    foreach (object o in animals)
                    {
                        invoker.Invoke(o);
                    }
                }

                timer.Stop();
                Console.WriteLine("{0} took {1}", invoker.GetType().Name, timer.Elapsed);
            }
        }
    }

    public interface INop
    {
        void Nop();
    }

    public class Dog : INop
    {
        public void Speak()
        {
            Console.WriteLine("Woof"); ;
        }

        public  void Nop()
        {
            
        }
    }

    public class Duck : INop
    {
        public void Speak()
        {
            Console.WriteLine("Quack"); ;
        }

        public  void Nop()
        {

        }

    }

    public class Sheep : INop
    {
        public void Speak()
        {
            Console.WriteLine("Baaa");
        }

        public  void Nop()
        {

        }
    }
}
