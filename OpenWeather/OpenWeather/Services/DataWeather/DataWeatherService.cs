using System.Threading.Tasks;
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

        public async Task<WeatherMainModel> GetWeather(string city)
        {
            var getWeatherDetails = await _restService.GetAllWeathers(city);
            return getWeatherDetails;
        }
    }
}
