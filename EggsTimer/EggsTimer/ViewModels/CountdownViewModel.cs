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
            DateTime d = DateTime.Now;
            DateTime currentDate = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
            TimerModel timerModel = new TimerModel()
            {
                Name = Enum.GetName(typeof(EggsCookingTime), time), 
                StartTime = currentDate,
                Duration = time,
                IsStarted = true
            };
            timerModel = await timerService.Create(timerModel);
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