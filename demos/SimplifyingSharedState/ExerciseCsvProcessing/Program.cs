using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSVFileProcessing;

namespace ExerciseCsvProcessing
{
    public class Person
    {
        public Person(string name)
        {
            Console.WriteLine("Created");
            Name = name;
        }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0} Age {1}", Name,Age);
        }
    }

    class Program
    {
        static void LazyMain(string[] args)
        {
            Console.WriteLine("Main entered");

            var andy = new Lazy<Person>(() => new Person("Andy"));

            Console.WriteLine("Initializing Andy");
           // andy.Value.Name = "Andy";
            andy.Value.Age = 21;

            Console.WriteLine(andy);
        }

        static void Main(string[] args)
        {
            int loadCount = 0;
            Func<string, StreamReader> createStreamAndCount = file =>
            {
                Interlocked.Increment(ref loadCount);
                return new StreamReader(file);
            };

            var repository = new CsvRepository(@"..\..\Data",createStreamAndCount);


            Parallel.For(0, 10, i =>
             {
                 foreach (string file in repository.Files)
                {
                    var weatherEnties = repository.Map(file, row => new WeatherEntry(row)).ToList();
                    Console.WriteLine("{0} has {1} entries", file, weatherEnties.Count);
                }
            });
           

            Console.WriteLine();

            Console.WriteLine("Loaded {0}" , loadCount);
        }
    }
}
