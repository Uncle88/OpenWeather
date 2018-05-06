using CoreLocation;
using Foundation;
using MapKit;
using OpenWeather.Controls;
using OpenWeather.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;

[assembly: ExportRenderer(typeof(ExtMap), typeof(ExtMapRenderer))]
namespace OpenWeather.iOS.Renderer
{
    public class ExtMapRenderer : MapRenderer
    {
        private readonly UITapGestureRecognizer _tapRecogniser;

        public ExtMapRenderer()
        {
            _tapRecogniser = new UITapGestureRecognizer(OnTap)
            {
                NumberOfTapsRequired = 1,
                NumberOfTouchesRequired = 1
            };
        }
        IMKAnnotation annotation;

        private void OnTap(UITapGestureRecognizer recognizer)
        {
            
            var cgPoint = recognizer.LocationInView(Control);

            var location = ((MKMapView)Control).ConvertPoint(cgPoint, Control);

            ((ExtMap)Element).OnTap(new Position(location.Latitude, location.Longitude));
            var control = Control as MKMapView;
            if (annotation != null)
            {
                control.RemoveAnnotation(annotation);
                annotation = new BasicMapAnnotation(new CLLocationCoordinate2D(location.Latitude, location.Longitude), "Pin", "Selected Place");
                control.AddAnnotation(annotation);
            }
            else
            {
                annotation = new BasicMapAnnotation(new CLLocationCoordinate2D(location.Latitude, location.Longitude), "Pin", "Selected Place");
                control.AddAnnotation(annotation);
            }
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
        {
            if (Control != null)
                Control.RemoveGestureRecognizer(_tapRecogniser);
            
            base.OnElementChanged(e);

            if (Control != null)
                Control.AddGestureRecognizer(_tapRecogniser);
        }
    }

    class BasicMapAnnotation : MKAnnotation
    {
        CLLocationCoordinate2D coord;
        string title, subtitle;

        public override CLLocationCoordinate2D Coordinate { get { return coord; }}
        public override void SetCoordinate(CLLocationCoordinate2D value)
        {
            coord = value;
        }
        public override string Title { get { return title; } }
        public override string Subtitle { get { return subtitle; } }
        public BasicMapAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
        {
            this.coord = coordinate;
            this.title = title;
            this.subtitle = subtitle;
        }
    }
}
