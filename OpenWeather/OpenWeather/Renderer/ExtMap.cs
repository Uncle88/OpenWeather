using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.DataWeather;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.Renderer
{
    public class ExtMap : Map
    {
        private readonly IDataWeatherService _dataWeatherService;

        public ExtMap()
        {
            _dataWeatherService = new DataWeatherService();
        }

        public static readonly BindableProperty OnTappedProperty =
            BindableProperty.Create(nameof(OnTap), typeof(bool), typeof(ExtMap), false);

        public Task OnTap
        {
            get { return (Task)GetValue(OnTappedProperty); }
            set { SetValue(OnTappedProperty, value); }
        }

        private Command _onMapTapCommand;
        public Command OnMapTapCommand
        {
            get
            {
                return _onMapTapCommand ?? (_onMapTapCommand = new Command(async (_) =>
                {
                    await GetPosition();
                    await GetWeatherByCoordinates(position);
                }));
            }
        }

        public Plugin.Geolocator.Abstractions.Position position { get; set; }

        private WeatherMainModel _weatherMainModel;
        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                IconImageString = "http://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png";
                OnPropertyChanged();
            }
        }

        private string _iconImageString;
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> GetPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            position = await locator.GetPositionAsync();
            return position;
        }

        private async Task<WeatherMainModel> GetWeatherByCoordinates(Plugin.Geolocator.Abstractions.Position position)
        {
            WeatherMainModel = await _dataWeatherService.GetWeatherByGeoCoordinate(position.Latitude, position.Longitude);
            return WeatherMainModel;
        }
    }
}