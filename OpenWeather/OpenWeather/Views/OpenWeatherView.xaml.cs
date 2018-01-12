using OpenWeather.ViewModels;
using Xamarin.Forms;

namespace OpenWeather
{
    public partial class OpenWeatherView : ContentPage
    {
        public OpenWeatherView()
        {
            InitializeComponent();
            BindingContext = new OpenWeatherViewModel();
        }
    }
}
