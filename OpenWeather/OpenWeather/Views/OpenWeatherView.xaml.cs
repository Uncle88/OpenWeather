using OpenWeather.ViewModels;
using OpenWeather.Views;
using Xamarin.Forms;

namespace OpenWeather
{
    public partial class OpenWeatherView : BaseView
    {
        public OpenWeatherView()
        {
            InitializeComponent();
            BindingContext = new OpenWeatherViewModel(Navigation);
        }

        public static readonly BindableProperty ColorBackgroundProperty =
            BindableProperty.Create(nameof(ColorBackground), typeof(bool), typeof(OpenWeatherView), false);

        public Color ColorBackground
        {
            get { return (Color)GetValue(ColorBackgroundProperty); }
            set { SetValue(ColorBackgroundProperty, value); }
        }
    }
}
