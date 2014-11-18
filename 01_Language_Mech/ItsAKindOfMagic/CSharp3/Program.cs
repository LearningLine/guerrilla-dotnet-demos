using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharp3.UtilStuff;

namespace CSharp3
{
    class CoolHero
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Power: {1}", Name, Power);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero{Name = "batman", Power=2, IsCool = true},
                new SuperHero{Name = "superman", Power=9, IsCool = false},
                new SuperHero{Name = "captain america", Power=3, IsCool = true},
                new SuperHero{Name = "spiderman", Power=6, IsCool = true},
                new SuperHero{Name = "wonder woman", Power=10, IsCool = true},
                new SuperHero{Name = "hancock", Power=6, IsCool = false},
            };

            var coolHeros = new List<CoolHero>();

            //coolHeros = heroes.Where(h => h.IsCool)
            //                  .Select(h => new CoolHero
            //                  {
            //                    Name = h.Name,
            //                    Power = h.Power
            //                  }).ToList();

            coolHeros = (from h in heroes
                        where h.IsCool
                        select new CoolHero
                              {
                                Name = h.Name,
                                Power = h.Power
                              } ).ToList();

            // SELECT Name, Power from heroes WHERE IsCool = true

            coolHeros.ForEach(Console.WriteLine);

            int[] ints = { 1, 9, 1, 7, 1 };
            ints.Where(i => i > 1).ForEach(Console.WriteLine);



//            foreach (SuperHero hero in heroes)
//            {
////                Console.WriteLine(StringUtils.Capitalise(hero.Name));
//                Console.WriteLine((hero.Name.Capitalise()));
//            }
        }

        private static void Extensions(IEnumerable<SuperHero> heroes)
        {
            heroes.ForEach(h => Console.WriteLine(h.Name.Capitalise()));

            int[] ints = {1, 9, 1, 7, 1};
            ints.ForEach(Console.WriteLine);

            var i = new {Name = "Hulk", Power = 10, IsCool = true};

            Console.WriteLine(i);
        }

        private static void PropInitializers()
        {
            SuperHero batman = null;
            try
            {
                batman = new SuperHero();

                batman.Name = "batman";
                batman.Power = -2;
                batman.IsCool = true;
            }
            catch
            {
            }

            Console.WriteLine(batman);

            SuperHero robin = null;
            try
            {
                robin = new SuperHero
                {
                    Name = "robin",
                    Power = -1,
                    IsCool = false,
                };
            }
            catch
            {
            }
            Console.WriteLine(robin);
        }
    }

    class SuperHero
    {
        private int _power;
        public string Name { get; set; }

        public int Power
        {
            get { return _power; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Not a superhero", "Power");
                }
                _power = value;
            }
        }

        public bool IsCool { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Power: {1}, IsCool: {2}", Name, Power, IsCool);
        }
    }
}
