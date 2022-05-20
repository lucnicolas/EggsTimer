using System.ComponentModel;
using System.Runtime.CompilerServices;
using EggsTimer.Services;

namespace EggsTimer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected const string StartTimer = "StartTimer";
        protected const string CancelTimer = "CancelTimer";

        protected static readonly TimerService timerService = new TimerService();
        protected static readonly MessagingService messagingService = new MessagingService();
        protected static readonly NavigationService navigationService = new NavigationService();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string name = "")
        {
            //TODO Vérifier l'égalité
            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}