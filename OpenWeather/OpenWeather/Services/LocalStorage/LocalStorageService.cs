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
        private const string FileName = "file.txt";
        private const string FolderName = "Cache";
        private readonly RestService _restService;

        public LocalStorageService()
        {
            _restService = new RestService();
        }

        private Task<IFolder> CreateFolder()
        {
			IFolder rootFolder = FileSystem.Current.LocalStorage;
            return rootFolder.CreateFolderAsync(FolderName, CreationCollisionOption.OpenIfExists);
        }

        public async void ClearStorage()
        {
            var folder = await CreateFolder();
            if (folder == null)
                return;
            
            ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(FileName);

            if (isFileExisting != ExistenceCheckResult.NotFound)
            {
                try
                {
                    IFile file = await folder.CreateFileAsync(FileName,CreationCollisionOption.OpenIfExists);
                    await file.DeleteAsync();
                }
                catch(Exception ex)
                {
                    return ;
                }
            }
        }

        public async Task<WeatherMainModel> PCLReadStorage<WeatherMainModel>()
        {
            var folder = await CreateFolder();
            ExistenceCheckResult isFileExisting = await folder.CheckExistsAsync(FileName);

            if (isFileExisting != ExistenceCheckResult.NotFound)
            {
                try
                {
                    IFile file = await folder.CreateFileAsync(FileName,CreationCollisionOption.OpenIfExists);

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

            try
            {
                var folder = await CreateFolder();
                IFile file = await folder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(doc.ToString());
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
