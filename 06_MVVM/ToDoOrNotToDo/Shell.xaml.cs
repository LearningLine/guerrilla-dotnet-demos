using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoOrNotToDo.ViewModels;
using ToDoOrNotToDo.Views;

namespace ToDoOrNotToDo
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();

            
            Func<string, bool> confirm =
                m => MessageBox.Show(m, "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        
            Content = new ToDoEditorViewModel(confirm);

        }
    }
}
