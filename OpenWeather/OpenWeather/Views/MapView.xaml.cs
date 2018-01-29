using OpenWeather.ViewModels;
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
        }
    }
}
