using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace SimpleXAML
{

    public class Person : DependencyObject
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} is {1}", this.Name, this.Age);
        }
    }

    [ContentProperty("Employees")]
    public class Company
    {
        public string Name { get; set; }
        public Person Owner { get; set; }

        public List<Person> Employees { get; set; }

        public static DependencyProperty EmployeeIdProperty =
            DependencyProperty.RegisterAttached("EmployeeId",
                typeof(int), typeof(Company));

        public static void SetEmployeeId(Person p, int id)
        {
           p.SetValue(Company.EmployeeIdProperty, id);
        }
        public static int GetEmployeeId(Person p)
        {
            return (int) p.GetValue(Company.EmployeeIdProperty);
        }

        public Company()
        {
            Employees = new List<Person>();
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, Owner: {1}", 
                Name, Owner);
        }
    }

    public class RandomValue : MarkupExtension
    {
        private static Random rnd = new Random();
        public int Min { get; set; }
        public int Max { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return rnd.Next(Min, Max);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //using (var file = File.OpenRead(@"..\..\person.xml"))
            //{
            //    Person p = (Person) XamlReader.Load(file);
            //    Console.WriteLine(p);
            //}

            using (var file = File.OpenRead(@"..\..\company.xml"))
            {
                Company c = (Company)XamlReader.Load(file);
                Console.WriteLine(c);
                foreach (var e in c.Employees)
                {
                    Console.WriteLine("  {0}  {1}  ", 
                        Company.GetEmployeeId(e), e);
                }
            }
        }
    }
}
