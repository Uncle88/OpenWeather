using System;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace OpenWeather.ViewModels
{
    public class FavoritePlacesViewModel
    {
        public FavoritePlacesViewModel()
        {
        }

        public async Task GetFavoriteForecast()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
        }
    }
}
