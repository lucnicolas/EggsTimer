using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EggsTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimerView : ContentPage
    {
        public TimerView()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}