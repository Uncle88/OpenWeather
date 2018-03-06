using System;
using Xamarin.Forms.Maps;

namespace OpenWeather.Controls
{
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}