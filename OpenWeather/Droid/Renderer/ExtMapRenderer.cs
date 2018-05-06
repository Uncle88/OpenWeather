using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using OpenWeather.Controls;
using OpenWeather.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMapRenderer))]
namespace OpenWeather.Droid.Renderer
{
    public class ExtMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;

        public ExtMapRenderer(Context context) : base(context){}

		protected override MarkerOptions CreateMarker(Pin pin)
		{
            return base.CreateMarker(pin);
		}

		public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
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
            var marker = new MarkerOptions();
            _map.Clear();
            marker.SetPosition(new LatLng(e.Point.Latitude, e.Point.Longitude));
            marker.SetTitle("Selected Place");
            _map.AddMarker(marker);
        }
    }


}
