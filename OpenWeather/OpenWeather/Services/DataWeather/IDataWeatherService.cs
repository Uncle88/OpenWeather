using System.Threading.Tasks;
using OpenWeather.Models;

namespace OpenWeather.Services.DataWeather
{
    public interface IDataWeatherService
    {
        Task<WeatherMainModel> GetWeatherByCityName(string cityName);
        Task<WeatherMainModel> GetWeatherByGeoCoordinate(double  Latitude, double Longitude);
    }
}
