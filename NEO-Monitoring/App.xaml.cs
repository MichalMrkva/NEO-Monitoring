using Android.Content.Res;
using Android.Widget;
using NEOMonitoring.Views;
using Plugin.Connectivity;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NEO_Monitoring
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Preferences.Get("Loged", false))
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new APIKeyPage();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
