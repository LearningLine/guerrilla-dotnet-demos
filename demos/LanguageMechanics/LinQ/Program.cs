using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LinQ
{
    static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        //public static IEnumerable<T> Where<T>(this IEnumerable<T> source, 
        //                                           Func<T, bool> test )
        //{

        //   // Console.WriteLine("MINE!!!");
        //    foreach (T item in source)
        //    {
        //        if (test(item))
        //            yield return item;
        //    }
        //}

        public static IEnumerable<TResult> Select<TSource,TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> project )
        {
            foreach (TSource item in source)
            {
                yield return project(item);
            }
        }

        public static List<T> ToList<T>(this IEnumerable<T> items)
        {
            return new List<T>(items);
        }

    }
    static class Util
    {
        public static string Capitalize(this string source)
        {
            return source.Substring(0, 1).ToUpper() + source.Substring(1);
        }

        public static void Times(this int nTimes, Action action)
        {
            for (int i = 0; i < nTimes; i++)
            {
                action();
            }
        }
    }

    class CoolHero
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0}, Power: {1}", Name, Power);
        }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<SuperHero> heroes = GetHeroes();

            //new List<int>() { 23,345,1,2345,4}.Where(n => n%2 == 0)
            //    .ForEach(Console.WriteLine);

            
            //var coolHeroes = heroes.Where(h => h.IsCool)
            //                       .Select(h => new CoolHero {Name = h.Name, Power = h.Power})
            //                       .ToList();


            var coolHeroes = (from hero in GetHeroes()
                              where hero.IsCool
                              select new CoolHero {Name = hero.Name, Power = hero.Power})
                                  .ToList();
            //foreach (SuperHero hero in heroes)
            //{
            //    if (hero.IsCool)
            //    {
            //        coolHeroes.Add(new CoolHero {Name = hero.Name, Power = hero.Power});
            //    }
            //}

            coolHeroes.ForEach(Console.WriteLine);

            // SELECT Name, Power FROM heroes WHERE IsCool = true
        }

        private static void AnonTypes()
        {
//var map = new Dictionary<int, List<string>>();

            var batman = new {Name = "Batman", Power = 10, IsCool = true};

            Console.WriteLine(batman.GetType());

            var spiderman = new {Name = "spiderman", IsCool = true, Power = 1};

            Console.WriteLine(spiderman.GetType());
        }

        private static void MoreExtensions()
        {
            int[] values = new int[]
            {
                231,
                45,
                1,
                45,
                4,
                21,
                3463,
            };

            values.ForEach(Console.WriteLine);

            // 10.Times(() => Console.WriteLine("Hello world"));
        }

        private static void ExtensionMethods(IEnumerable<SuperHero> heroes)
        {
            foreach (SuperHero hero in heroes)
            {
                //  Console.WriteLine(Util.Capitalize(hero.Name));
                Console.WriteLine(hero.Name.Capitalize());
            }
        }

        private static IEnumerable<SuperHero> GetHeroes()
        {
            List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero {Name = "spiderman", Power = 1, IsCool = true},
                new SuperHero {Name = "hulk", Power = 9, IsCool = true},
                new SuperHero {Name = "batman", Power = 10, IsCool = true},
                new SuperHero {Name = "wonderwoman", Power = 11, IsCool = true},
                new SuperHero {Name = "robin", Power = 1, IsCool = false},
                new SuperHero {Name = "superman", Power = 10, IsCool = false},
            };

            return heroes;
        }

        private static void ObjectInitializers()
        {
            SuperHero spiderman = null;
            try
            {
                spiderman = new SuperHero();

                spiderman.Name = "Spidey";
                spiderman.Power = -1;
                spiderman.IsCool = true;
            }
            catch (Exception)
            {
                // move along - nothing to see here
            }

            Console.WriteLine(spiderman);

            SuperHero hulk = null;
            try
            {
                SuperHero temp = new SuperHero();
                temp.Name = "HULK";
                temp.Power = -9;
                temp.IsCool = true;

                hulk = temp;
            }
            catch (Exception)
            {
            }

            Console.WriteLine(hulk);
        }
    }

    class SuperHero
    {
        public override string ToString()
        {
            return String.Format("Name: {0}, Power: {1}, IsCool: {2}", Name, Power, IsCool);
        }

        private int power;
        public string Name { get; set; }

        public int Power
        {
            get { return power; }
            set
            {
                if(value < 1) throw new ArgumentException("Not powerful enough", "Power");
                power = value;
            }
        }

        public bool IsCool { get; set; }
    }
}
