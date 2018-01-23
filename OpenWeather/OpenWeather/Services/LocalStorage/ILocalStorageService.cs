using System;
using System.Threading.Tasks;
using OpenWeather.Models;
using PCLStorage;

namespace OpenWeather.Services.LocalStorage
{
    public interface ILocalStorageService
    {
        Task PCLWriteStorage(WeatherMainModel _weatherMainModel);
        Task<WeatherMainModel> PCLReadStorage();
    }
}
