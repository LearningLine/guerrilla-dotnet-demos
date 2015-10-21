using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BadLibs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeClock _clock;
        private StoryEngine _engine = new StoryEngine { DelayMs = 1 };
        private CancellationTokenSource _cancelSource;

        public MainWindow()
        {
            InitializeComponent();

            _clock = new TimeClock(this, () => Time.Text = DateTime.Now.ToLongTimeString());
        }
        
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var storyText = _engine.CreateStory(StorySelect.SelectedIndex + 1);

            StoryText.Inlines.Clear();
            StoryText.Inlines.AddRange(storyText.Select(ts =>
            {
                var run = new Run(ts.Text);
                if (ts.Format)
                {
                    run.TextDecorations.Add(TextDecorations.Underline);
                    run.FontFamily = new FontFamily("Segoe Print");
                }
                return run;
            }));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_cancelSource != null)
                _cancelSource.Cancel();
        }
    }
}