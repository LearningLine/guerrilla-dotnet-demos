using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoOrNotToDo.ViewModels;

namespace ToDoOrNotToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ToDoItemViewModel
            {
                Title = "I must learn to type",
                Priority = 1
            };
        }

        private void SetBindings()
        {
            var todo = new ToDoItemViewModel
            {
                Title = "I must learn to type",
                Priority = 3
            };

            var titleBinding = new Binding("Title")
            {
                Source = todo
            };

            TitleTextBox.SetBinding(TextBox.TextProperty, titleBinding);

            var priorityBinding = new Binding("Priority")
            {
                Source = todo
            };

            PrioritySlider.SetBinding(Slider.ValueProperty, priorityBinding);

            var blockBinding = new Binding("PriorityAsString")
            {
                Source = todo
            };

            PriorityBlock.SetBinding(TextBlock.TextProperty, blockBinding);
        }
    }
}


















