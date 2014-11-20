using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplicationHelloWorld.Annotations;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace WpfApplicationHelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random random = new Random();
        readonly StringToS stringToS;
        private int currentPosition = 0;

        public MainWindow()
        {
            InitializeComponent();

            stringToS = new StringToS { TargetString = "enum", CurrentCorrectString = "", CurrentWrongString = "", CurrentProgress = Brushes.LawnGreen };
            DataContext = stringToS;
        }

        private void NextString()
        {
            stringToS.CurrentCorrectString = "";
            stringToS.CurrentWrongString = "";
            var index = random.Next(strings.Count);
            var s = strings[index];
            stringToS.TargetString = s.ToLower();
            stringToS.CurrentProgress = Brushes.LawnGreen;
            currentPosition = 0;
        }
        SoundPlayer soundPlayer = new SoundPlayer("doh1_y.wav");

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {

                NextString();
                return;
            }
            //e.ImeProcessedKey == Key.Enter

            //throw new NotImplementedException();

            if (currentPosition >= stringToS.TargetString.Length) return;

            var keyData = e.Key;
            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(keyData).ToLower();

            if (stringToS.TargetString[currentPosition] == keyChar[0])
            {
                stringToS.CurrentCorrectString += keyChar;
                stringToS.CurrentWrongString += " ";
            }
            else
            {
                stringToS.CurrentWrongString += stringToS.TargetString[currentPosition];
                stringToS.CurrentCorrectString += " ";
                stringToS.CurrentProgress = Brushes.Red;
                soundPlayer.Play();

            }

            currentPosition++;
        }

        private List<string> strings = new List<string>
        {
            "int",
            "enum",
            "enumerable",
            "delegate",
            "dictionary",
            "string",
            "ctor",
            "class",
            "tuborg",
            "interface",
            "abstract",
            "xaml",
            "application",
            "binding",
        };
    }

    /*public class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;

        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }*/

    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StringToS : ViewModel
    {
        private string _currentString;
        private string _targetString;
        private string _currentWrongString;
        private SolidColorBrush _currentProgress;

        public string TargetString
        {
            get { return _targetString; }
            set { _targetString = value; OnPropertyChanged(); }
        }

        public string CurrentCorrectString
        {
            get { return _currentString; }
            set { _currentString = value; OnPropertyChanged(); }
        }

        public string CurrentWrongString
        {
            get { return _currentWrongString; }
            set { _currentWrongString = value; OnPropertyChanged(); }
        }

        public SolidColorBrush CurrentProgress
        {
            get { return _currentProgress; }
            set { _currentProgress = value; OnPropertyChanged(); }
        }
    }
}
