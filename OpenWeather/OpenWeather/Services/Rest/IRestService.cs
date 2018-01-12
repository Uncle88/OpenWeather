using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenWeather.Models;

namespace OpenWeather.Services.Rest
{
    public interface IRestService
    {
        Task<JContainer> GetDataFromService(string queryString);
    }
}
