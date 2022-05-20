using System.Threading.Tasks;
using System.Windows.Input;
using EggsTimer.Models;
using Xamarin.Forms;

namespace EggsTimer.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        public TimerViewModel()
        {
            StartBoiledCommand = new Command(async () => await StartCountdownAsync(EggsCookingTime.Boiled));
            StartSoftBoiledCommand = new Command(async () => await StartCountdownAsync(EggsCookingTime.SoftBoiled));
            StartHardBoiledCommand = new Command(async () => await StartCountdownAsync(EggsCookingTime.HardBoiled));
            HistoryCommand = new Command(async () => await navigationService.PushAsync("HistoryView"));
        }

        public ICommand StartBoiledCommand { get; set; }
        public ICommand StartSoftBoiledCommand { get; set; }
        public ICommand StartHardBoiledCommand { get; set; }
        public ICommand HistoryCommand { get; set; }

        public async Task StartCountdownAsync(EggsCookingTime time)
        {
            await navigationService.PushAsync("CountdownView");
            messagingService.Send(StartTimer, time);
        }
    }
}