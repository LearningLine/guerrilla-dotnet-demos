using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace XAMLApi
{
    [ContentProperty("Employees")]
    public class Company
    {
        public static readonly DependencyProperty EmployeeIdProperty =
            DependencyProperty.RegisterAttached("EmployeeId", typeof (int), typeof (Company));

        public static void SetEmployeeId(DependencyObject obj, int val)
        {
            obj.SetValue(EmployeeIdProperty, val);
        }

        public static int GetEmployeeId(DependencyObject obj)
        {
            return (int)obj.GetValue(EmployeeIdProperty);
        }

        public Company()
        {
            Employees = new List<Person>();
        }
        public string Name { get; set; }
        public Person Owner { get; set; }

        public List<Person> Employees { get; set; }
    }
}