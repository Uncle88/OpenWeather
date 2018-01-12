using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenWeather.Models;

namespace OpenWeather.Services.DataWeather
{
    public interface IDataWeatherService
    {
        Task<Weather> GetWeather();
    }
}
