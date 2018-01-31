using System;
using OpenWeather.ViewModels;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.Views
{
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();

            //CurrentLocation();
        }

        private async void CurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(1)));
        }
    }
}
