using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.DataWeather;
using OpenWeather.Services.LocalStorage;
using OpenWeather.Services.Rest;

namespace OpenWeather.ViewModels
{
    public class OpenWeatherViewModel : ViewModelBase
    {
        private readonly IDataWeatherService _dataWeatherService;
        private readonly IRestService _restService;
        private readonly ILocalStorageService _localStorageService;

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
                if (_city != value)
                {
                    if (value != null)
                    {
                        _city = value;
                        Task.Run(async () =>
                        {
                            await InitializeGetWeatherAsync();
                        });
                        OnPropertyChanged();
                    }
                    else
                    {
                        //read from localstorage
                        Task.Run(async () =>
                        {
                            await InitializeGetWeatherAsync2();
                        });
                        OnPropertyChanged();
                    }
                }
            }
        }

        private async Task InitializeGetWeatherAsync2()
        {
            try
            {
                IsBusy = true;
                WeatherMainModel = await _localStorageService.PCLReadStorage();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(1000);
                WeatherMainModel = await _dataWeatherService.GetWeatherByCityName(_city);

                //write to localstorage
                await _localStorageService.PCLWriteStorage(WeatherMainModel);
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
