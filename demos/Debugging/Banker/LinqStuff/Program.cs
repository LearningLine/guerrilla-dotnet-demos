using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LinqStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\demos");

            var names = from d in dir.GetDirectories()
                        orderby d.Name
                        select new { d.Name, d.LastAccessTime };

            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
        }
    }
}
