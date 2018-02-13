using OpenWeather.ViewModels;

namespace OpenWeather.Views
{
    public partial class MapView : BaseView
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();
            MapViewModel.Map = MyMap;
        }
    }
}
