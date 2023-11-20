using Android.Widget;
using NEOMonitoring.API;
using NEOMonitoring.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NEO_Monitoring
{
    public partial class MainPage : ContentPage
    {
        MainPageContent content;
        public MainPage()
        {
            content = new MainPageContent();
            content.NEOCollection = new List<NearEarthObject>();
            InitializeComponent();
            BindingContext = content;
            content.Date = DateTime.Today.AddDays(-1);

        }
    }
    class MainPageContent : INotifyPropertyChanged
    {
        private string _Element_count_displayed;
        public string Element_count_displayed
        {
            get => $"Number of objects: {_Element_count_displayed}";
            set 
            { 
                _Element_count_displayed = value;
                OnPropertyChanged("Element_count_displayed");
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get=>_date;
            set
            {
                if (value > DateTime.Today.AddDays(-1))
                {
                    value = DateTime.Today.AddDays(-1);
                }
                _date = value;
                _=SetNEOsAsync(value, value);
                OnPropertyChanged("Date");
            }
        }
        private List<NearEarthObject> _NEOCollection;
        public List<NearEarthObject> NEOCollection
        {
            get => _NEOCollection;
            set
            {
                _NEOCollection = value;
                OnPropertyChanged("NEOCollection");
            }
        }
        private NearEarthObject _NEOSelected;
        public NearEarthObject NEOSelected
        { 
            get => _NEOSelected;
            set 
            {
                if (value != null)
                {
                    _NEOSelected = null;
                    OnPropertyChanged("NEOSelected");
                    Application.Current.MainPage.Navigation.PushAsync(new NEODetail(value));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public async Task SetNEOsAsync(DateTime fromDate, DateTime toDate)
        {
            await Task.Run(() =>
            {
                var NEOs = NEOMAPI.GetNEO(fromDate, toDate);
                var list = new List<NearEarthObject>();
                if (NEOs != null)
                {
                    Element_count_displayed = NEOs.Element_count.ToString();
                    foreach (var obj in NEOs.Near_earth_objects)
                    {
                        foreach (var item in obj.Value)
                        {
                            list.Add(item);
                        }
                        break;
                    }

                }
                else
                {
                    string[] lastload = NEOMAPI.LastLoadGet();
                    if (lastload != null)
                        if (lastload[0] != null)
                        {
                            _date = DateTime.Parse(lastload[0]);
                            OnPropertyChanged("Date");
                            NEOs = JsonConvert.DeserializeObject<NearEarthObjects>(lastload[1]);
                            Element_count_displayed = NEOs.Element_count.ToString();
                            foreach (var obj in NEOs.Near_earth_objects)
                            {
                                foreach (var item in obj.Value)
                                {
                                    list.Add(item);
                                }
                                break;
                            }
                            Toast.MakeText(Android.App.Application.Context, "Aktuální data nejdou načíst", ToastLength.Short).Show();
                        }
                    Toast.MakeText(Android.App.Application.Context, "Unknown error", ToastLength.Short).Show();
                }
                NEOCollection = null;
                NEOCollection = list;
            });
        }
    }
}
