using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}