using System.Threading.Tasks;
using OpenWeather.Constants;
using OpenWeather.Models;
using OpenWeather.Services.Rest;

namespace OpenWeather.Services.DataWeather
{
    public class DataWeatherService : IDataWeatherService
    {
        public IRestService _restService;

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
            return $"{WeatherConstants.BaseUrl}weather?q={cityName}&appid={WeatherConstants.key}";
        }
    }
}
