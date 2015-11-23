using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new PubsEntities())
            {
                foreach (var title in ctx.GetTitlesByCountrySlim("USA"))
                {
                    Console.WriteLine(title.title);
                }
            //    foreach (var publisher in ctx.Publishers.Include(p => p.titles)
            //                                 .Where(p => p.country == "USA"))
            //    {
            //        Console.WriteLine(new Publisher().GetType());
            //        Console.WriteLine(publisher.GetType());
            //        foreach (Title title in publisher.titles)
            //        {
            //            Console.WriteLine("{0} costs {1:C}", title.Name, title.price);
            //            if (title.price != null)
            //            {
            //                title.price /= 2;
            //            }
            //        }
            //    }
            //    Console.WriteLine("waiting ...");
            //    Console.ReadLine();

            //    ctx.SaveChanges();
            }
        }

        private static void ReadingStuff()
        {
            using (var ctx = new PubsEntities())
            {
                string country = "USA";
                foreach (Publisher publisher in ctx.Publishers
                    .OrderBy(p => p.pub_name.ToLower())
                    .Where(p => p.country != country))
                {
                    Console.WriteLine(publisher.pub_name);
                }
            }
        }
    }
}
