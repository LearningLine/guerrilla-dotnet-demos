using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleBinding
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1  : Window
    {
        private Person person = new Person() { Name = "Andy", Age = 21 };

        public Window1()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(PersonEditCommands.Save, SavePerson));
            CommandBindings.Add(new CommandBinding(PersonEditCommands.Birthday, Birthday));

            DataContext = person;
            //nameTextBox.SetBinding(TextBox.TextProperty,
            //    new Binding("Name") {Source = person});

            //ageTextBox.SetBinding(TextBox.TextProperty,
            //    new Binding("Age") { Source = person, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

        }




        private void SavePerson(object sender, ExecutedRoutedEventArgs args)
        {
          
            MessageBox.Show(person.ToString());
        }

        private void Birthday(object sender, ExecutedRoutedEventArgs args)
        {
            person.Age++;
        }
    }
}



