using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using EggsTimer.Models;
using EggsTimer.Services;
using Xamarin.Forms;

namespace EggsTimer.ViewModels
{
    public class CountdownViewModel : BaseViewModel
    {
        private readonly VibratorService vibratorService = new VibratorService();

        private bool isCanceled = false;
        
        private TimeSpan counter;
        public TimeSpan Counter 
        {
            get
            {
                return counter;
            }
            set
            {
                if (counter != value)
                    SetProperty(ref counter, value);
            } 
        }
        
        public ICommand CancelCommand { get; set; }

        public CountdownViewModel()
        {
            CancelCommand = new Command(async () => await CancelAsync());
            messagingService.Subscribe<EggsCookingTime>(this, StartTimer, async (time) =>
            {
                await StartAsync(time);
                messagingService.Unsubscribe<EggsCookingTime>(this, StartTimer);
            });
        }
        
        public async Task StartAsync(EggsCookingTime time)
        {
            TimerModel timer = await timerService.Create(time);   
            Counter = TimeSpan.FromSeconds((int)timer.Time);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (isCanceled)
                    return false;
                double totalSeconds = Counter.TotalSeconds;
                if (totalSeconds <= 0)
                {
                    vibratorService.Vibrate();
                    return false;
                }
                Counter = TimeSpan.FromSeconds(totalSeconds-1);
                return true;
            });
        }
        
        public async Task CancelAsync()
        {
            isCanceled = true;
            await navigationService.PopAsync();
        }
        
    }
}