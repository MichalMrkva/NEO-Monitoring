using NEO_Monitoring;
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
	public partial class NEODetail : ContentPage
	{
        NEODetailContent content = new NEODetailContent();
        public NEODetail(NearEarthObject selectedNEO)
		{
            BindingContext = content;
            InitializeComponent();
        }
    }
    public class NEODetailContent : INotifyPropertyChanged
    {
        public ICommand ImgBtnClicked { get; set; } = new Command(() => 
        { 
            Application.Current.MainPage.Navigation.PopAsync();
        });
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}