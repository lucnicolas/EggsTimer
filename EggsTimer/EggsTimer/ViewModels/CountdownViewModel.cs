using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using EggsTimer.Models;
using EggsTimer.Services;
using Xamarin.Forms;
using Timer = System.Timers.Timer;

namespace EggsTimer.ViewModels
{
    public class CountdownViewModel : BaseViewModel
    {
        private readonly VibratorService vibratorService = new VibratorService();

        private Timer timer;
        private string cTimer;
        private TimeSpan counter;

        private bool isCanceled;

        public CountdownViewModel()
        {
            CancelCommand = new Command(async () => await CancelAsync());
            messagingService.Subscribe<int>(this, StartTimer, async id =>
            {
                await StartAsync(id);
                messagingService.Unsubscribe<int>(this, StartTimer);
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

        public async Task StartAsync(int id)
        {
            DateTime d = DateTime.Now;
            DateTime currentDate = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);

            TimerModel timerModel = await timerService.Read(id);
            DateTime endTime = currentDate.AddSeconds((int)timerModel.Duration);

            Counter = (endTime - currentDate);
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