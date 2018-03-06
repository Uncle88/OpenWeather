using System;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.Controls
{
    public class ExtMap : Map
    {
        public ExtMap(){ }

        public ExtMap(MapSpan region)
            : base(region) { }

        public static readonly BindableProperty CurrentPositionProperty =
            BindableProperty.Create(nameof(CurrentPosition),
                                    typeof(Plugin.Geolocator.Abstractions.Position),
                                    typeof(ExtMap),
                                    new Plugin.Geolocator.Abstractions.Position(48.45, 35),
                                    propertyChanged: HandleBindingPropertyChangedDelegate);

        public Plugin.Geolocator.Abstractions.Position CurrentPosition
        {
            get { return (Plugin.Geolocator.Abstractions.Position)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ExtMap map && oldValue != null)
            {
                var temp = (Plugin.Geolocator.Abstractions.Position)oldValue;
                var position = MapSpan.FromCenterAndRadius(new Position(temp.Latitude, temp.Longitude), Distance.FromKilometers(5));
                map.MoveToRegion(position);
            }
        }

        public event EventHandler<MapTapEventArgs> Tapped;

        private Plugin.Geolocator.Abstractions.Position _currentPos;
        public Plugin.Geolocator.Abstractions.Position CurrentPos
        {
            get { return _currentPos; }
            set
            {
                _currentPos = value;
                OnPropertyChanged();
            }
        }

        public async void OnTap(Position coordinate)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            CurrentPos = await locator.GetPositionAsync();
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
                handler(this, e);
        }
    }
}