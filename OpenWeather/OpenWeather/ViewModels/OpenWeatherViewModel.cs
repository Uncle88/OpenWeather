using System;
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
            City = "DNIPRO";

            _dataWeatherService = new DataWeatherService();
            _restService = new RestService();

            GetForecast();
        }

        private async Task<Weather> GetForecast()
        {
            var awaited = await _dataWeatherService.GetWeather();
            return awaited;
        }

        private string _city;
        private string _temperature;
        private DateTime _dateTimeRequest;
        private string _wind;
        private string _cloudiness;
        private string _pressure;
        private string _humidity;
        private string _sunrise;
        private string _sunset;
        private string _geoCoord;

        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateTimeRequest
        {
            get { return _dateTimeRequest; }
            set
            {
                if (_dateTimeRequest != value)
                {
                    _dateTimeRequest = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Wind
        {
            get { return _wind; }
            set
            {
                if (_wind != value)
                {
                    _wind = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Cloudiness
        {
            get { return _cloudiness; }
            set
            {
                if (_cloudiness != value)
                {
                    _cloudiness = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Pressure
        {
            get { return _pressure; }
            set
            {
                if (_pressure != value)
                {
                    _pressure = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Humidity
        {
            get { return _humidity; }
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Sunrise
        {
            get { return _sunrise; }
            set
            {
                if (_sunrise != value)
                {
                    _sunrise = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Sunset
        {
            get { return _sunset; }
            set
            {
                if (_sunset != value)
                {
                    _sunset = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GeoCoord
        {
            get { return _geoCoord; }
            set
            {
                if (_geoCoord != value)
                {
                    _geoCoord = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
