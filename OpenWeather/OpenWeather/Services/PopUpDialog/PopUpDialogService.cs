using OpenWeather.Models;
using Xamarin.Forms;

namespace OpenWeather.Services.PopUpDialog
{
    public class PopUpDialogService : IPopUpDialogService
    {
        public void ShowMessage(WeatherMainModel weatherMainModel)
        {
            DependencyService.Get<IPopUpDialogService>().ShowMessage(weatherMainModel);
        }
    }
}
