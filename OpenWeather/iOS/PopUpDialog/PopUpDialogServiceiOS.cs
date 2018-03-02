using System;
using OpenWeather.Models;
using OpenWeather.Services.PopUpDialog;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(OpenWeather.iOS.PopUpDialog.PopUpDialogServiceiOS))]
namespace OpenWeather.iOS.PopUpDialog
{
    public class PopUpDialogServiceiOS : IPopUpDialogService
    {
        public void ShowMessage(WeatherMainModel weatherMainModel)
        {
            UIAlertView alert = new UIAlertView()
            {
                Title = "Current forecast",
                Message = weatherMainModel.name,
            };
            alert.AddButton("OK");
            alert.Show();
        }
    }
}
