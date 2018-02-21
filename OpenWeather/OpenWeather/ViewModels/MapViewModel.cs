using System;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(){}

        public static Map Map { get; set; }

        public async override void Initialize()
        {
            var locator = CrossGeolocator.Current;
            TimeSpan ts = TimeSpan.FromMilliseconds(10000);
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(100)));
        }

        private Command _clickStandard;
        public Command ClickStandard
        {
            get
            {
                return _clickStandard ?? (_clickStandard = new Command((_) =>
                {
                    Map.MapType = MapType.Street;
                }));
            }
        }

        private Command _clickHybrid;
        public Command ClickHybrid
        {
            get
            {
                return _clickHybrid ?? (_clickHybrid = new Command((_) =>
                {
                    Map.MapType = MapType.Hybrid;
                }));
            }
        }

        private Command _clickSatellite;
        public Command ClickSatellite
        {
            get
            {
                return _clickSatellite ?? (_clickSatellite = new Command((_) =>
                {
                    Map.MapType = MapType.Satellite;
                }));
            }
        }
    }
}
