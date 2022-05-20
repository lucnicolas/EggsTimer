using System;
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

        private TimeSpan counter;

        private bool isCanceled;

        public CountdownViewModel()
        {
            CancelCommand = new Command(async () => await CancelAsync());
            messagingService.Subscribe<EggsCookingTime>(this, StartTimer, async time =>
            {
                await StartAsync(time);
                messagingService.Unsubscribe<EggsCookingTime>(this, StartTimer);
            });
        }

        public TimeSpan Counter
        {
            get => counter;
            set
            {
                if (counter != value)
                    SetProperty(ref counter, value);
            }
        }

        public ICommand CancelCommand { get; set; }

        public async Task StartAsync(EggsCookingTime time)
        {
            var timer = await timerService.Create(time);
            Counter = TimeSpan.FromSeconds((int) timer.Time);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (isCanceled)
                    return false;
                var totalSeconds = Counter.TotalSeconds;
                if (totalSeconds <= 0)
                {
                    vibratorService.Vibrate();
                    return false;
                }

                Counter = TimeSpan.FromSeconds(totalSeconds - 1);
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