using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EggsTimer.Models;
using EggsTimer.Services;
using Xamarin.Forms;

namespace EggsTimer.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {

        public ICommand StartBoiledCommand { get; set; }
        public ICommand StartSoftBoiledCommand { get; set; }
        public ICommand StartHardBoiledCommand { get; set; }

        public TimerViewModel()
        {
            StartBoiledCommand = new Command(async () => await StartAsync(EggsCookingTime.Boiled));
            StartSoftBoiledCommand = new Command(async () => await StartAsync(EggsCookingTime.SoftBoiled));
            StartHardBoiledCommand = new Command(async () => await StartAsync(EggsCookingTime.HardBoiled));
        }

        public async Task StartAsync(EggsCookingTime time)
        {
            await navigationService.PushAsync("CountdownView");
            messagingService.Send(StartTimer, time);

        }
    }
}