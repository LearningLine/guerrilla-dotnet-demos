using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

// Show dynamic in action, then say well how does it work
// perhaps it uses reflection, do that
// Caches the MethodInfo
// Hmmm....reflection invoke obviously still too slow...
// Introduce fast invoker.


namespace Dynamic
{

    public interface IDynamicInvoke<T>
    {
        T Invoke(object target, params object[] args);
    }

    public class SudoDynamic<T> : IDynamicInvoke<T>

    {
          Dictionary<Type,FastInvoke>  methodCache = new Dictionary<Type, FastInvoke>();
        private readonly string _method;

        public SudoDynamic(string method)
        {
            _method = method;
        }

        public T Invoke(object target, params object[] args)
        {
            Type targetType = target.GetType();
            FastInvoke methodToInvoke = null;

            if (!methodCache.TryGetValue(targetType,out methodToInvoke))
            {
                 methodToInvoke = new FastInvoke(targetType,_method);
                methodCache[targetType] = methodToInvoke;
            }
            

            return (T)methodToInvoke.ExecuteDelegate(target, args);
        }
    }

public class ReflectionDynamic<T> : IDynamicInvoke<T>
    {
        Dictionary<Type,MethodInfo>  methodCache = new Dictionary<Type, MethodInfo>();
        private readonly string _method;

        public ReflectionDynamic(string method)
        {
            _method = method;
        }

        public T Invoke(object target, params object[] args)
        {
            Type targetType = target.GetType();
            MethodInfo methodToInvoke = null;

            if (!methodCache.TryGetValue(targetType,out methodToInvoke))
            {
                 methodToInvoke = targetType.GetMethod(_method);
                methodCache[targetType] = methodToInvoke;
            }
            

            return (T)methodToInvoke.Invoke(target, args);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var animals = new object[] { new Dog(), new Duck(), new Sheep() };

         
          //  SimpleDynamic();

            DynamicInvokers(animals);

            PureDynamic(animals);

            InterfaceInvokers(animals);
        }

        private static void SimpleDynamic()
        {
            dynamic i = 5;
            i++;
            Console.WriteLine(i);

            i = "hello";
            Console.WriteLine(i.ToUpper());

            //     i.Speak();

            dynamic expando = new ExpandoObject();

            expando.Name = "Rich";
            expando.IsSexy = false;

            Console.WriteLine(expando.Name);
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
                                        new ReflectionDynamic<object>("Nop"), 
                                        new SudoDynamic<object>("Nop"), 
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
