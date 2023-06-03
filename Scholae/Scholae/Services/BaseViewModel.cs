using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Scholae.Services
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public BaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
