using System.Threading.Tasks;
using OpenWeather.Models;

namespace OpenWeather.Services.Rest
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(string city);
    }
}
