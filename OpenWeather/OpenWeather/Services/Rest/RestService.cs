using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenWeather.Constants;
using OpenWeather.Models;

namespace OpenWeather.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<T> GetAsync<T>(string city)
        {
            using( var client = new HttpClient())
            {
                using (var responce = await client.GetAsync(WeatherConstants.OpenWeatherApi + city + "&APPID=" + WeatherConstants.key))
                {
                    var json = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
    }
}
