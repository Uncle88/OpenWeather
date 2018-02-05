using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
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

        public async void ClearStorage()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Cache",CreationCollisionOption.OpenIfExists);
            ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(".txt");

            if (!isFileExisting.ToString().Equals("NotFound"))//!
            {
                try
                {
                    IFile file = await folder.CreateFileAsync(".txt",CreationCollisionOption.OpenIfExists);
                    await file.DeleteAsync();
                }
                catch
                {
                    return ;
                }
            }
        }

        public async Task<WeatherMainModel> PCLReadStorage<WeatherMainModel>()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Cache",CreationCollisionOption.OpenIfExists);
            ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(".txt");

            if (isFileExisting.ToString().Equals("NotFound"))
            {
                try
                {
                    IFile file = await folder.CreateFileAsync(".txt",CreationCollisionOption.OpenIfExists);

                    String languageString = await file.ReadAllTextAsync();

                    XmlSerializer oXmlSerializer = new XmlSerializer(typeof(WeatherMainModel));
                    return (WeatherMainModel)oXmlSerializer.Deserialize(new StringReader(languageString));
                }
                catch(Exception ex)
                {
                    return default(WeatherMainModel);
                }
            }
            return default(WeatherMainModel);
        }

        public async Task PCLWriteStorage<WeatherMainModel>(WeatherMainModel _weatherMainModel)
        {
            XDocument doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                var serializer = new XmlSerializer(typeof(WeatherMainModel));
                serializer.Serialize(writer, _weatherMainModel);
            }

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Cache",CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(".txt",CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(doc.ToString());
        }
    }
}
