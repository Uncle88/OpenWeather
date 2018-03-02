using Android.App;
using OpenWeather.Models;
using OpenWeather.Services.PopUpDialog;
using Xamarin.Forms;

[assembly: Dependency(typeof(OpenWeather.Droid.PopUpDialog.PopUpDialogServiceDroid))]
namespace OpenWeather.Droid.PopUpDialog
{
    public class PopUpDialogServiceDroid : IPopUpDialogService
    {
        public void ShowMessage(WeatherMainModel weatherMainModel)
        {
            {
                AlertDialog.Builder bilder = new AlertDialog.Builder(null);
                AlertDialog alertdialog = bilder.Create();
                alertdialog.SetTitle("Current forecast");
                alertdialog.SetMessage(weatherMainModel.name);
                alertdialog.Show();
            }
        }
    }
}