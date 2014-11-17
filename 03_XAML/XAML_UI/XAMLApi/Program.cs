using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;

namespace XAMLApi
{
    class Program
    {
        static void Main(string[] args)
        {
           // Person p = (Person) XamlServices.Load(@"..\..\person.xml");

            var c = (Company) XamlServices.Load(@"..\..\company.xml");

            Console.WriteLine(c.Name);
            Console.WriteLine(c.Owner);

            foreach (var emp in c.Employees)
            {
                Console.WriteLine("\t{0} : {1}", emp, Company.GetEmployeeId(emp));
                
            }
        }
    }

    public class RandomAge : MarkupExtension
    {
        public int Min { get; set; }
        public int Max { get; set; }
        static Random rnd = new Random();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return rnd.Next(Min, Max);
        }
    }

}
