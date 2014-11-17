using System.Windows;

namespace XAMLApi
{
    public class Person : DependencyObject
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} is {1}", Name, Age);
        }
    }
}