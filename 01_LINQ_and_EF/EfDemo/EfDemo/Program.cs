using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDemo
{
    public class Pet
    {
        [Key]
        public int Key { get; set; }

        public string Name { get; set; }

        //public Person Person { get; set; }
    }

    public class Person
    {
        public Person()
        {
            Pets = new HashSet<Pet>();
        }

        [Key]
        public int Key { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        public string Lastname { get; set; }
        
        [Range(0, Int32.MaxValue)]
        public int Age { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class PeopleContext : DbContext
    {
        public PeopleContext()
            : base()
        {
        }

        public PeopleContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Person> People { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PeopleContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<());

            using(var ctx = new PeopleContext("foo"))
            {
                ctx.Database.Log = s =>
                {
                    Console.WriteLine("Log: {0}", s);
                    Console.WriteLine();
                };

                var brock = ctx.People.Include("Pets").First();
                //var brock = ctx.People.First();
                Console.WriteLine(brock.GetType().FullName);
                Console.WriteLine(brock.Name);
                foreach(var pet in brock.Pets)
                {
                    Console.WriteLine("\t{0}", pet.Name);
                }

                brock.Name = "Not Brock";
                ctx.SaveChanges();


                //Console.WriteLine(ctx.Database.Connection.ConnectionString);

                //var query =
                //    from p in ctx.People
                //    where p.Age < 20
                //    select new { p.Name, p.Age };

                //var results = query.ToArray();

                //Console.WriteLine("Found: {0}", results.Count());

                //Console.WriteLine("Query results:");
                //foreach (var item in results)
                //{
                //    Console.WriteLine(item);
                //}


                //foreach(var person in ctx.People)
                //{
                //    Console.WriteLine("{0}, {1}", person.Name, person.Age);
                //}

                //Console.WriteLine("Now adding people...");
                //for (var i = 0; i < 10; i++)
                //{
                //    var brock = new Person
                //    {
                //        Name = "Person " + i.ToString(),
                //        Age = 3 * i,
                //        LastUpdated = DateTime.UtcNow
                //    };
                //    ctx.People.Add(brock);
                //}

                //var brock = new Person
                //{
                //    Name = "Brock",
                //    Age = 31,
                //    LastUpdated = DateTime.UtcNow
                //};
                //brock.Pets.Add(new Pet { Name = "Oscar" });
                //brock.Pets.Add(new Pet { Name = "Tar" });
                //ctx.People.Add(brock);
                //ctx.SaveChanges();

                //Console.WriteLine("Brock was added");

            } // ctx.Dispose();
        }
    }
}
