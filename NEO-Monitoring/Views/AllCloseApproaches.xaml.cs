using NEOMonitoring.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NEOMonitoring.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllCloseApproaches : ContentPage
    {
        AllCloseApproachesContent content;
        public AllCloseApproaches(NearEarthObject selectedNEO)
        {
            content = new AllCloseApproachesContent(selectedNEO);
            BindingContext = content;
            InitializeComponent();
        }
    }
    public class AllCloseApproachesContent : INotifyPropertyChanged
    {
        private string url;
        public AllCloseApproachesContent(NearEarthObject nearEarthObject)
        {
            ACACollection = new List<FormatedApproachData>();
            url = nearEarthObject.Links.Self;
            Task.Run(FormatCAPAsync);
        }
        private async Task FormatCAPAsync()
        {
            await Task.Run(() =>
            {
                var list = NEOMAPI.GetCloseApproachData(url);
                var ACAC = new List<FormatedApproachData>();
                foreach (var item in list)
                {
                    ACAC.Add(new FormatedApproachData() { Date= item.Close_approach_date, MissDistance = item.Miss_distance.Kilometers});
                }
                ACACollection = null;
                ACACollection = ACAC;
            });
        }
        public ICommand ImgBtnClicked { get; set; } = new Command(() =>
        {
            Application.Current.MainPage.Navigation.PopAsync();
        });
        private List<FormatedApproachData> _ACACollection;
        public List<FormatedApproachData> ACACollection {
            get {  return _ACACollection; }
            set {  _ACACollection = value; OnPropertyChanged("ACACollection"); }
        } 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}