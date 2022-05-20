using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EggsTimer.Models;
using Xamarin.Forms;

namespace EggsTimer.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
          private TimerModel selected;
        private bool isRefreshing;

        public TimerModel Selected
        {
            get => selected;
            set
            {
                selected = value;
                SetProperty(ref selected, null);
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public Command RefreshCommand { get; }

        public ObservableCollection<TimerModel> Items { get; } = new ObservableCollection<TimerModel>();

        public HistoryViewModel()
        {
            RefreshCommand = new Command(async () => await RefreshAsync());
            
            messagingService.Subscribe<TimerModel>(this, CancelTimer, (model) => Items.Remove(model));

            RefreshCommand.Execute(null);
        }

        public async Task RefreshAsync()
        {
            IsRefreshing = true;

            Items.Clear();
            foreach (var item in await timerService.Read())
                Items.Add(item);

            IsRefreshing = false;
        }
    }
}