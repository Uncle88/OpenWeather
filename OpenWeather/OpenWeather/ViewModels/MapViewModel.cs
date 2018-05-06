using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public delegate Task SetParamsDelegate(Position pos);

    public class MapViewModel : ViewModelBase
    {
        public static event SetParamsDelegate ParametersSet;

        public MapViewModel(){}

        public MapViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public async override void Initialize()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 10;
            CurrentPos = await locator.GetPositionAsync();
        }

        private Plugin.Geolocator.Abstractions.Position _currentPos;
        public Plugin.Geolocator.Abstractions.Position CurrentPos
        {
            get { return  _currentPos; }
            set 
            {
                _currentPos = value;
                OnPropertyChanged();
            }
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get { return _selectedPosition;}
            set
            {
                if (_selectedPosition != null)
                {
                    _selectedPosition = new Position(0,0);
                    _selectedPosition = value;
                }
                _selectedPosition = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; }

        private ICommand _clickPinWeather;
        public ICommand ClickPinWeather
        {
            get
            {
                return _clickPinWeather ?? (_clickPinWeather = new Command( async (object obj) => 
                {
                    await Navigation.PopAsync();
                    CallEvent();
                }));
            }
        }

        private void CallEvent()
        {
            MapViewModel.ParametersSet(SelectedPosition);
        }
    }
}
