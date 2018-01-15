using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenWeather.Constants;
using OpenWeather.Models;

namespace OpenWeather.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<WeatherMainModel> GetAllWeathers(string city)
        {
            HttpClient _httpClient = new HttpClient();
            var json = await _httpClient.GetStringAsync(WeatherConstants.OpenWeatherApi + city + "&APPID=" + WeatherConstants.key);
            var getWeatherModels = JsonConvert.DeserializeObject<WeatherMainModel>(json);
            return getWeatherModels;
        }
    }
}
