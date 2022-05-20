using EggsTimer.Services;
using EggsTimer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace EggsTimer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationService.AddView(nameof(CountdownView), typeof(CountdownView));
            NavigationService.AddView(nameof(HistoryView), typeof(HistoryView));

            MainPage = new NavigationPage(new TimerView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}