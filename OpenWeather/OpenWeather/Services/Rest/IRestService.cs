using System.Threading.Tasks;

namespace OpenWeather.Services.Rest
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(string url);
    }
}
