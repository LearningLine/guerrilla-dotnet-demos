using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PropertyInitialisers
{
    public class Person
    {
        public string Name { get; set; }

        private int age;

        public int Age
        {
            get { return age; }
            set { if (value < 0) throw new ArgumentException("Bad age");  age = value; }
        }

        public override string ToString()
        {
            return String.Format("Name {0} age {1}", Name, Age);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
}
