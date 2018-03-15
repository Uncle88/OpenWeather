using System.Collections.Generic;
using System.Threading;
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
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public OpenWeatherViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _dataWeatherService = new DataWeatherService();
            _restService = new RestService();
            _localStorageService = new LocalStorageService();

            MapViewModel.ParametersSet += HandlerParameterSet;
        }

        public override async void Initialize()
        {
            var token = _cts.Token;
            await ReadFromPCLStorage(token);
        }

        public Position _selectPosition { get; set; }

        private async Task HandlerParameterSet(Position pos)
        {
            _cts.Cancel();
            _selectPosition = pos;
            await DataWeatherFromSelectedPlace();
            OnPropertyChanged();
        }

        public async Task DataWeatherFromSelectedPlace()
        {
            WeatherMainModel = await _dataWeatherService.GetWeatherByGeoCoordinate(_selectPosition.Latitude, _selectPosition.Longitude);
        }

        public async Task DataWeatherFromGeoLocator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();

            WeatherMainModel = await _dataWeatherService.GetWeatherByGeoCoordinate(position.Latitude, position.Longitude);
        }

        private async Task ReadFromPCLStorage(CancellationToken token)
        {
            IsBusy = true;

            var getStorageResult = await _localStorageService.PCLReadStorage<WeatherMainModel>();

            if (token.IsCancellationRequested)
            {
                IsBusy = false;
                return;
            }

            if (getStorageResult != null)
            {
                WeatherMainModel = getStorageResult;
                await Task.Delay(3000);
                IsBusy = false;

                if (token.IsCancellationRequested)
                    return;

                await DataWeatherFromGeoLocator();
                OnPropertyChanged();
                WriteToPCLStorage();

            }
            else
            {
                await DataWeatherFromGeoLocator();
                OnPropertyChanged();
                IsBusy = false;
                WriteToPCLStorage();
            }
        }

        private void WriteToPCLStorage()
        {
            _localStorageService.ClearStorage();
            _localStorageService.PCLWriteStorage(WeatherMainModel);
        }

        private Color _colorBackground;
        public Color ColorBackground
        {
            get { return _colorBackground; }
            set
            {
                _colorBackground = value;
                OnPropertyChanged();
            }
        }

        private void ChangeBackgroundColor()
        {
            if (WeatherMainModel.weather[0].description == "clear sky")
            {
                ColorBackground = Color.DeepSkyBlue;
            }
            else if (WeatherMainModel.weather[0].description == "few clouds")
            {
                ColorBackground = Color.Coral;
            }
            else if (WeatherMainModel.weather[0].description == "scattered clouds")
            {
                ColorBackground = Color.BurlyWood;
            }
            else if (WeatherMainModel.weather[0].description == "broken clouds")
            {
                ColorBackground = Color.Brown;
            }
            else if (WeatherMainModel.weather[0].description == "shower rain")
            {
                ColorBackground = Color.Azure;
            }
            else if (WeatherMainModel.weather[0].description == "rain")
            {
                ColorBackground =Color.Aqua;
            }
            else if (WeatherMainModel.weather[0].description == "thunderstorm")
            {
                ColorBackground = Color.BlueViolet;
            }
            else if (WeatherMainModel.weather[0].description == "show")
            {
                ColorBackground =Color.Blue;
            }
            else if (WeatherMainModel.weather[0].description == "mist")
            {
                ColorBackground = Color.Silver;
            }
            else
            {
                ColorBackground = Color.White;
            }
        }

        public INavigation Navigation { get; internal set; }

        private WeatherMainModel _weatherMainModel; 
        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                ChangeBackgroundColor();
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
                    _isBusy = value;
                    OnPropertyChanged();
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