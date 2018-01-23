using System;
using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.Rest;
using PCLStorage;

namespace OpenWeather.Services.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly RestService _restService;

        public LocalStorageService()
        {
            _restService = new RestService();
        }

        public async Task<WeatherMainModel> PCLReadStorage()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync("answer.txt");

            string _text = null;
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync("answer.txt");
                _text = await file.ReadAllTextAsync();  
            }
            return (WeatherMainModel)Convert.ChangeType(_text, typeof(WeatherMainModel));
        }

        public async Task PCLWriteStorage(WeatherMainModel _weatherMainModel)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("answer.txt", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(_weatherMainModel.ToString());
        }
    }
}
