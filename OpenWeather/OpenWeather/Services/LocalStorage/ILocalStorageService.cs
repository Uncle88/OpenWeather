using System.Threading.Tasks;

namespace OpenWeather.Services.LocalStorage
{
    public interface ILocalStorageService
    {
        Task PCLWriteStorage<WeatherMainModel>(WeatherMainModel _weatherMainModel);
        Task<WeatherMainModel> PCLReadStorage<WeatherMainModel>();
        void ClearStorage();
    }
}
