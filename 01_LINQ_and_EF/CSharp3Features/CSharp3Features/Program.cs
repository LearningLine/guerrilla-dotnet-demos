using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp3Features
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    static class PersonHelpers
    {
        public static void PrintPerson(this Person person)
        {
            Console.WriteLine("The person's name is {0} and they are {1} years old", person.Name, person.Age);
        }
    }

    class Program
    {
        static bool IsOld(Person p)
        {
            return p.Age >= 30;
        }

        static void Main(string[] args)
        {
            // LINQ
            var people = new Person[]{
                new Person{Name = "Brock", Age = 42},
                new Person{Name = "Michael", Age = 41},
                new Person{Name = "Pete", Age = 32},
                new Person{Name = "Ken", Age = 12},
            };

            //IEnumerable<Person> oldFolks = Enumerable.Where(people, IsOld);
            //var oldFolks = Enumerable.Where(people, p => p.Age >= 40);

            // select age from people where age >= 30
            //var oldFolks = people
            //    .Where(p => p.Age >= 40)
            //    .OrderBy(p => p.Age)
            //    .ThenByDescending(x=>x.Name)
            //    //.ThenByDescending(x=>x.)
            //    .Select(x => new { Name = x.Name, Age = x.Age });

            var oldFolks =
                from p in people
                where p.Age < 40
                orderby p.Age, p.Name descending
                select new { p.Age, p.Name };

            var list = oldFolks.ToList();
            list.Add(new { Age = 10, Name = "Whatever" });

            foreach(var item in list)
            {
                Console.WriteLine(item);
            }

        }

        delegate int MathOp(int x, int y);

        static int Add(int a, int b)
        {
            return a + b;
        }

        static void OldMain(string[] args)
        {
            //MathOp op = Add;
            
            //int result = op(2, 3);
            //Console.WriteLine(result);

            //op = delegate(int x, int y)
            //{
            //    return x * y;
            //};
            //result = op(2, 3);
            //Console.WriteLine(result);

            //op = (x, y) => x - y;
            //result = op(2, 3);
            //Console.WriteLine(result);

            //op = (x, y) =>
            //{
            //    return x + y;
            //};
            //result = op(2, 3);
            //Console.WriteLine(result);

            //DeclareThis foo = GetSkjhfkjhdfkjhfkj();

            var brock = new Person
            {
                Name = "Brock",
                Age = 12
            };
            //PersonHelpers.PrintPerson(brock);
            brock.PrintPerson();
            //Console.WriteLine("The person's name is {0} and they are {1} years old", brock.Name, brock.Age);
            //Console.WriteLine(brock.GetType().FullName);

            var michael = new
            {
                Name = "Michael",
                Age = 12,
                LastName = "Kennedy"
            };
            //Console.WriteLine(michael.GetType().FullName);
            //michael.l
            //var steve = new
            //{
            //    Name = "Steve",
            //    Age = 12,
            //    LastName = "Kennedy"
            //};
            //Console.WriteLine(michael.GetType() == steve.GetType());
        }
    }
}
