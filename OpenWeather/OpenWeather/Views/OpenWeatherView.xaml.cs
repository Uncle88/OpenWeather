using OpenWeather.ViewModels;
using OpenWeather.Views;
using Xamarin.Forms;

namespace OpenWeather
{
    public partial class OpenWeatherView : BaseView// ContentPage//BaseView
    {
        public OpenWeatherView()
        {
            InitializeComponent();
            BindingContext = new OpenWeatherViewModel(Navigation);
        }
    }
}
