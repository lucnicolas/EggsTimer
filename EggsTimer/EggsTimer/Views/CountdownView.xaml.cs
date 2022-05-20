using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EggsTimer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountdownView : ContentPage
    {
        public CountdownView()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}