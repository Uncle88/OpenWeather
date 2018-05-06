using System;
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
            if (232 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 200)
            {
                ColorBackground = Color.Blue;
            }
            else if (321 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 300)
            {
                ColorBackground = Color.DeepSkyBlue;
            }
            else if (531 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 500)
            {
                ColorBackground = Color.Aquamarine;
            }
            else if (622 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 600)
            {
                ColorBackground = Color.Azure;
            }
            else if (781 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 701)
            {
                ColorBackground = Color.LightGray;
            }
            else if ( WeatherMainModel.weather[0].id == 800)
            {
                ColorBackground = Color.LightYellow;
            }
            else if (804 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 801)
            {
                ColorBackground = Color.LightSlateGray;
            }
            else if (906 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 900)
            {
                ColorBackground = Color.CornflowerBlue;
            }
            else if (962 >= WeatherMainModel.weather[0].id && WeatherMainModel.weather[0].id >= 951)
            {
                ColorBackground = Color.GreenYellow;
            }
            else
            {
                ColorBackground = Color.Red;
            }
        }

        public void ParseResponce()
        {
            Temperature = WeatherMainModel.main.temp;
            CityName = WeatherMainModel.name;
            Country = WeatherMainModel.sys.country;
            Humidity = WeatherMainModel.main.humidity;
            WindSpeed = WeatherMainModel.wind.speed;
            Pressure = WeatherMainModel.main.pressure;
            MainStatus = WeatherMainModel.weather[0].main;
            MainStatusDescription = WeatherMainModel.weather[0].description;
            SunRise = (WeatherMainModel.sys.sunrise).ToString();
            SunSet = (WeatherMainModel.sys.sunset).ToString();
            CurrentTime = DateTime.Now.ToString();
        }

        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        private double _temperature;
        public double Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private double _humidity;
        public double Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        private double _windSpeed;
        public double WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                _windSpeed = value;
                OnPropertyChanged();
            }
        }

        private double _pressure;
        public double Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                OnPropertyChanged();
            }
        }

        private string _mainStatus;
        public string MainStatus
        {
            get { return _mainStatus; }
            set
            {
                _mainStatus = value;
                OnPropertyChanged();
            }
        }

        private string _mainStatusDescription;
        public string MainStatusDescription
        {
            get { return _mainStatusDescription; }
            set
            {
                _mainStatusDescription = value;
                OnPropertyChanged();
            }
        }

        private string _sunRise;
        public string SunRise
        {
            get { return _sunRise; }
            set
            {
                _sunRise = value;
                OnPropertyChanged();
            }
        }

        private string _sunSet;
        public string SunSet
        {
            get { return _sunSet; }
            set
            {
                _sunSet = value;
                OnPropertyChanged();
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
                ParseResponce();
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