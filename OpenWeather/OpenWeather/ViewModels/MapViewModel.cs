using System;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private double StepValue;

        public static Map Map { get; set; }
        public static Slider SliderMain { get; set; }

        public async override void Initialize()
        {
            var locator = CrossGeolocator.Current;
            TimeSpan ts = TimeSpan.FromMilliseconds(10000);
            locator.DesiredAccuracy = 50;
            await locator.StartListeningAsync(ts, 200);
            var position = await locator.GetPositionAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(100)));
            //Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.45, 35), Distance.FromKilometers(50)));

            //var zoomLevel = 5; // between 1 and 18
            //var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            //Map.MoveToRegion(new MapSpan(Map.VisibleRegion.Center, latlongdegrees, latlongdegrees));

            #region slider
            StepValue = 1.0;
            SliderMain = new Slider
            {
                Minimum = 0.0f,
                Maximum = 5.0f,
                Value = 0.0f,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            SliderMain.ValueChanged += OnSliderValueChanged;
            #endregion
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);
            SliderMain.Value = newStep * StepValue;
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
