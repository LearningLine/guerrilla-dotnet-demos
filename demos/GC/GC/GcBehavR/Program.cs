using System;
using System.Collections.Generic;

namespace GcBehavR
{
    class NeedToCleanMeUp : IDisposable
    {
        ~NeedToCleanMeUp()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Console.ReadLine();
            int count = 0;
            while (true)
            {
                count++;
                using (new NeedToCleanMeUp()) ;

                if (count % 20000000 == 0)
                    Console.Write(".");

            }

            //CreateGcMe();
           // Generations();

        }

        private static void Generations()
        {
            Random rand = new Random();
            object[] refs = new object[1000000];
            while (true)
            {
                refs[rand.Next(refs.Length)] = new object();
            }
        }

        private static void CreateGcMe()
        {
            for (int i = 0; i < 32000; i++)
            {
                new GcMe();
            }
        }
    }
}
