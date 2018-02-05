using OpenWeather.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.Views
{
    public partial class MapView : BaseView
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.45, 35), Distance.FromKilometers(100)));
        }
    }
}
