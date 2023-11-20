using Android.Content;
using Android.Widget;
using NEO_Monitoring;
using NEOMonitoring.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NEOMonitoring.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class APIKeyPage : ContentPage
    {
        APIKeyPageContent content;
        public APIKeyPage()
        {
            content = new APIKeyPageContent();
            BindingContext = content;
            
            InitializeComponent();
        }
    }
    public class APIKeyPageContent : INotifyPropertyChanged
    {
        public string EntryText { get; set; }
        public ICommand AddKeyPressed { get; set; }
        public ICommand LinkClicked { get; set; }
        public APIKeyPageContent()
        {
            AddKeyPressed = new Command(() =>
            {
                if (EntryText != null)
                {
                    Task.Run(KeyHandler);
                }
            });
            LinkClicked = new Command(async () => 
            {
                await Browser.OpenAsync("https://api.nasa.gov/");
            });
        }
        public async Task KeyHandler()
        {
            await Task.Run(() =>
            {
                var response = NEOMAPI.KeyStatus(EntryText);
                if (response == HttpStatusCode.OK)
                {
                    Preferences.Set("APIKey", EntryText);
                    Preferences.Set("Loged", true);
                    
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(new Action(() => 
                    {
                        Toast.MakeText(Android.App.Application.Context, "Key is OK", ToastLength.Short).Show();
                        Application.Current.MainPage = new NavigationPage(new MainPage()); 
                    }));

                }
                if (response == HttpStatusCode.Forbidden)
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(new Action(() =>
                    {
                        Toast.MakeText(Android.App.Application.Context, "Key is not valid", ToastLength.Short).Show();
                    }));
                if (response == HttpStatusCode.ServiceUnavailable)
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(new Action(() =>
                    {
                        Toast.MakeText(Android.App.Application.Context, "Server is not online", ToastLength.Short).Show();
                    }));
                else
                {
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(new Action(() =>
                    {
                        Toast.MakeText(Android.App.Application.Context, "Unknown error", ToastLength.Short).Show();
                    }));
                }
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}