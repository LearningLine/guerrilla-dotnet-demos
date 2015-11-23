using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonymousTypes
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var people = new []
            {
                new { Name = "Andy" , Age = 21  },
                new { Name = "Andy" , Age = 21 },
            };


            foreach (var p in people.Distinct())
            {
                Console.WriteLine(p);

            }
            

           
        }
    }
}
