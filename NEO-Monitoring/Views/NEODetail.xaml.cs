using NEOMonitoring.API;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace NEOMonitoring.Views
{
	public partial class NEODetail : ContentPage
	{
        NEODetailContent content;
        public NEODetail(NearEarthObject selectedNEO)
		{
            content = new NEODetailContent(selectedNEO);
            BindingContext = content;
            InitializeComponent();
        }
    }
    public class NEODetailContent : INotifyPropertyChanged
    {
        private NearEarthObject NEO;
        public NEODetailContent(NearEarthObject selectedNEO) 
        {
            NEO = selectedNEO;
            AllCAClicked = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushAsync(new AllCloseApproaches(selectedNEO));
            });
        }
        public string Name { get => NEO.Name; }
        public string ID { get => "ID: " + NEO.Id; }
        public string Time { get => "Time: " +  NEO.Close_approach_data[0].Close_approach_date_full; }
        public string DiaMin { get => "Min: " + NEO.Estimated_diameter.Kilometers.Estimated_diameter_min.ToString(); }
        public string DiaMax { get => "Max: " + NEO.Estimated_diameter.Kilometers.Estimated_diameter_max.ToString(); }
        public string RelVelKMS { get => "km/s: " + NEO.Close_approach_data[0].Relative_velocity.Kilometers_per_second; }
        public string RelVelKMH { get => "km/h: " + NEO.Close_approach_data[0].Relative_velocity.Kilometers_per_hour; }
        public string MissAstronomical { get => "Astronomical: " + NEO.Close_approach_data[0].Miss_distance.Astronomical; }
        public string MissLunar { get => "Lunar: " + NEO.Close_approach_data[0].Miss_distance.Lunar; }
        public string MissKilometers { get => "Kilometers: " + NEO.Close_approach_data[0].Miss_distance.Kilometers; }
        public string OrbitingBody { get => "Orbiting body: " + NEO.Close_approach_data[0].Orbiting_body; }
        public string IsSentryObject { get => "Is sentry object: " + NEO.Is_sentry_object; }
        public ICommand AllCAClicked { get; set; }
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