using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EggsTimer.Models;
using Xamarin.Forms;

namespace EggsTimer.ViewModels
{
    public class TimerViewModel : BaseViewModel
    {
        private TimerModel model;

        public TimerModel Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }
        public TimerViewModel()
        {
            StartCommand = new Command(async () => await StartCountdownAsync());
            HistoryCommand = new Command(async () => await navigationService.PushAsync("HistoryView"));
            Model = new TimerModel();
        }

        public ICommand StartCommand { get; set; }
        public ICommand HistoryCommand { get; set; }

        public async Task StartCountdownAsync()
        {
            double duration = Model.Duration;
            DateTime d = DateTime.Now;
            DateTime currentDate = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
            TimerModel timerModel = new TimerModel()
            {
                StartTime = currentDate,
                Duration = duration
            };
            timerModel = await timerService.Create(timerModel);
            
            await navigationService.PushAsync("CountdownView");
            messagingService.Send(StartTimer, timerModel.Id);
        }
    }
}