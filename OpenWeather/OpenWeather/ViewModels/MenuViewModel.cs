using System.Windows.Input;
using OpenWeather.Views;
using Xamarin.Forms;

namespace OpenWeather.ViewModels
{
    public class MenuViewModel
    {
        public ICommand GoWeatherCommand { get; set; }
        public ICommand GoMapCommand { get; set; }
        public ICommand GoFavoriteCommand { get; private set; }

        public MenuViewModel()
        {
            GoWeatherCommand = new Command(GoWeather);
            GoMapCommand = new Command(GoMap);
            GoFavoriteCommand = new Command(GoFavorite);
        }

        void GoWeather(object obj)
        {
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }

        void GoMap(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new MapView());
            App.MenuIsPresented = false;
        }

        void GoFavorite(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new FavoritePlacesView());
            App.MenuIsPresented = false;
        }
    }
}
