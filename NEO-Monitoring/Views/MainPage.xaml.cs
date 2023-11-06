using NEOMonitoring.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NEO_Monitoring
{
    public partial class MainPage : ContentPage
    {
        MainPageContent content = new MainPageContent();
        public MainPage()
        {
            BindingContext = content;
            content.Date = DateTime.Today.AddDays(-1);
            InitializeComponent();
        }
        async void TypeListSelected(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                var frame = (Frame)sender;
                var item = (NearEarthObject)frame.BindingContext;
            });
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
        public List<NearEarthObject> NEOCollection {
            get => _NEOCollection;
            set
            {
                _NEOCollection = value;
                OnPropertyChanged("NEOCollection");
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
                Element_count_displayed = NEOs.Element_count.ToString();
                var list = new List<NearEarthObject>();
                foreach (var obj in NEOs.Near_earth_objects)
                {
                    foreach (var item in obj.Value)
                    {
                        list.Add(item);
                    }
                    break;
                }
                NEOCollection = null;
                NEOCollection = list;
            });
        }
    }
}
