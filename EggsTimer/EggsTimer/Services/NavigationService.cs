using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EggsTimer.Services
{
    public class NavigationService
    {
        private static readonly Dictionary<string, Type> views = new Dictionary<string, Type>();

        public static void AddView(string name, Type type)
        {
            views[name] = type;
        }

        public async Task<Page> PopAsync()
        {
            Page currentPage = ((NavigationPage) Application.Current.MainPage).CurrentPage;
            return await currentPage.Navigation.PopAsync();
        }

        public async Task PushAsync(string viewName, params object[] args)
        {
            Type viewType = views[viewName];
            Page page = Activator.CreateInstance(viewType, args) as Page;

            Page currentPage = ((NavigationPage) Application.Current.MainPage).CurrentPage;
            await currentPage.Navigation.PushAsync(page);
        }
    }
}