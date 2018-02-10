using System;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel()
        {
        }

        public static Map Map { get; set; }

        public async override void Initialize()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetLastKnownLocationAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),Distance.FromKilometers(100)));
            //MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.45, 35), Distance.FromKilometers(100)));

            var zoomLevel = 2; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }
    }
}
