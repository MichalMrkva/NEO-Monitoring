using NEO_Monitoring;
using NEOMonitoring.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public APIKeyPageContent()
        {
            AddKeyPressed = new Command(() =>
            {
                if (EntryText != null)
                {
                    Task.Run(KeyHandler);
                }
            });
        }
        public async Task KeyHandler()
        {
            await Task.Run(() =>
            {
            if (NEOMAPI.IsKeyOK(EntryText))
            {
                Preferences.Set("APIKey", EntryText);
                Application.Current.Dispatcher.BeginInvokeOnMainThread(new Action(() => Application.Current.MainPage = new NavigationPage(new MainPage())));
            
                }
                else
                {

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