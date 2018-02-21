using Android.Gms.Maps;
using OpenWeather.Droid.CustomMapRenderer;
using OpenWeather.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMapRenderer))]
namespace OpenWeather.Droid.CustomMapRenderer
{
    public class ExtMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;

            base.OnElementChanged(e);

            if (Control != null)
                ((MapView)Control).GetMapAsync(this);
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((ExtMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}
