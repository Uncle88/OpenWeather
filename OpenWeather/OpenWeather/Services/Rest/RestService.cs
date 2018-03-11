﻿using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenWeather.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<T> GetAsync<T>(string url)
        {
            using( var client = new HttpClient())
            {
                using (var responce = await client.GetAsync(url))
                {
                    var json = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
    }
}
