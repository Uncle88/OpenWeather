using OpenWeather.ViewModels;
using OpenWeather.Views;

namespace OpenWeather
{
    public partial class OpenWeatherView : BaseView
    {
        public OpenWeatherView()
        {
            InitializeComponent();
            BindingContext = new OpenWeatherViewModel(Navigation);
        }
    }
}
