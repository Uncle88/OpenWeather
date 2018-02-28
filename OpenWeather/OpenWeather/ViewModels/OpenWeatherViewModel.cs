using System;
using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.DataWeather;
using OpenWeather.Services.LocalStorage;
using OpenWeather.Services.Rest;
using OpenWeather.Views;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public class OpenWeatherViewModel : ViewModelBase
    {
        private readonly IDataWeatherService _dataWeatherService;
        private readonly IRestService _restService;
        private readonly ILocalStorageService _localStorageService;

        public OpenWeatherViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _dataWeatherService = new DataWeatherService();
            _restService = new RestService();
            _localStorageService = new LocalStorageService();
        }

        public override async void Initialize()
        {
            await ReadFromPCLStorage();
        }

        public async Task DataWeatherFromGeoLocator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();

            WeatherMainModel = await _dataWeatherService.GetWeatherByGeoCoordinate(position.Latitude, position.Longitude);
        }

        private async Task ReadFromPCLStorage()
        {
            var getStorageResult = await _localStorageService.PCLReadStorage<WeatherMainModel>();
            if (getStorageResult != null)
            {
                WeatherMainModel = getStorageResult;
                await Task.Delay(3000);
                await DataWeatherFromGeoLocator();
                OnPropertyChanged();
                WriteToPCLStorage();

            }
            else
            {
                await DataWeatherFromGeoLocator();
                OnPropertyChanged();
                WriteToPCLStorage();
            }
        }

        private void WriteToPCLStorage()
        {
            _localStorageService.ClearStorage();
            _localStorageService.PCLWriteStorage(WeatherMainModel);
        }

        public INavigation Navigation { get; internal set; }

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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        private Command _clickCommandMove;
        public Command ClickCommandMove
        {
            get
            {
                return  _clickCommandMove = _clickCommandMove ?? new Command(async () =>
                    {
                    await Navigation.PushAsync(new MapView());
                });
            }
        }


    }
}