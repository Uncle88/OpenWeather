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

             var map = new Map(
             MapSpan.FromCenterAndRadius(
                     new Position(48.45, 35), Distance.FromKilometers(100)))
            {
                IsShowingUser = true,
                HeightRequest = 560,
                WidthRequest = 400,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            var slider = new Slider(0,5,0);
            stack.Children.Add(map);
            stack.Children.Add(slider);
            Content = stack;
        }
    }
}
