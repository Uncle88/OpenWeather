using System;
using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.Rest;

namespace OpenWeather.Services.DataWeather
{
    public class DataWeatherService : IDataWeatherService
    {
        private const string Key = "2e1694f1042f59269ae0ae384121b511";
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/";
        private readonly IRestService _restService;

        public DataWeatherService()
        {
            _restService = new RestService();
        }

        public Task<WeatherMainModel> GetWeatherByCityName(string cityName)
        {
            return _restService.GetAsync<WeatherMainModel>(CreateQueryStructureByCityName(cityName));
        }

        private string CreateQueryStructureByCityName(string cityName)
        {
            return $"{BaseUrl}weather?q={cityName}&appid={Key}&units=metric";
        }

        public Task<WeatherMainModel> GetWeatherByGeoCoordinate(double Latitude, double Longitude)
        {
            return _restService.GetAsync<WeatherMainModel>(CreateQueryStructureByGeoCoordinate(Latitude, Longitude));
        }

        private string CreateQueryStructureByGeoCoordinate(double latitude, double longitude)
        {
            return $"{BaseUrl}weather?lat={latitude}&lon={longitude}&appid={Key}&units=metric";
        }
    }
}
