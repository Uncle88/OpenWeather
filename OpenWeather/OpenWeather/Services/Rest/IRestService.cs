using System.Threading.Tasks;
using OpenWeather.Models;

namespace OpenWeather.Services.Rest
{
    public interface IRestService
    {
        Task<WeatherMainModel> GetAllWeathers(string city);
    }
}
