using System;
using Xamarin.Forms.Maps;

namespace OpenWeather.Renderer
{
    public class ExtMap : Map
    {
        public event EventHandler<MapTapEventArgs> Tapped;

        public ExtMap() { }

        public ExtMap(MapSpan region)
            : base(region) { }

        public void OnTap(Position coordinate)
        {
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