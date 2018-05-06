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

        public static readonly BindableProperty SelectedPositionProperty =
            BindableProperty.Create(nameof(SelectedPosition),
                                    typeof(Position),
                                    typeof(ExtMap),
                                     new Position(0,0));
        public Position SelectedPosition
        {
            get { return (Position)GetValue(SelectedPositionProperty); }
            set { SetValue(SelectedPositionProperty, value); }
        }

        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ExtMap map && oldValue != null)
            {
                var temp = (Plugin.Geolocator.Abstractions.Position)oldValue;
                var position = MapSpan.FromCenterAndRadius(new Position(temp.Latitude, temp.Longitude), Distance.FromKilometers(100));
                map.MoveToRegion(position);
            }
        }

        public void OnTap(Position coordinate)
        {
            SelectedPosition = coordinate;
        }
    }
}