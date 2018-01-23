using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenWeather.Services.Rest
{
    public class RestService : IRestService
    {
        public string json = null;
        public async Task<T> GetAsync<T>(string url)
        {
            using( var client = new HttpClient())
            {
                using (var responce = await client.GetAsync(url))
                {
                        json = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
    }
}
