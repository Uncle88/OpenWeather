using System.Windows.Input;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenWeather.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public async override void Initialize()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
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

        private MapType _selectedMapType;
        public MapType SelectedMapType
        {
            get { return _selectedMapType; }
            set 
            {
                _selectedMapType = value;
                OnPropertyChanged();
            }
        }

        private ICommand _clickStandard;
        public ICommand ClickStandard
        {
            get => _clickStandard ?? (_clickStandard = GetCommand(MapType.Street));
        }

        private ICommand _clickHybrid;
        public ICommand ClickHybrid
        {
            get => _clickHybrid ?? (_clickHybrid = GetCommand(MapType.Hybrid));
        }

        private ICommand _clickSatellite;
        public ICommand ClickSatellite
        {
            get => _clickSatellite ?? (_clickSatellite = GetCommand(MapType.Satellite));
        }

        private ICommand GetCommand(MapType mapType)
        {
            return new Command(() =>
            {
                SelectedMapType = mapType;
            });
        }
    }
}
