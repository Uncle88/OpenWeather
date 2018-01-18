using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.DataWeather;
using OpenWeather.Services.Rest;

namespace OpenWeather.ViewModels
{
    public class OpenWeatherViewModel : ViewModelBase
    {
        IDataWeatherService _dataWeatherService;
        IRestService _restService;

        public OpenWeatherViewModel()
        {
            _dataWeatherService = new DataWeatherService();
            _restService = new RestService();
        }

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
    
        private string _city; 
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Task.Run(async () => {
                    await InitializeGetWeatherAsync();
                });
                OnPropertyChanged();
            }
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true;
                WeatherMainModel = await _dataWeatherService.GetWeatherByCityName(_city);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}
