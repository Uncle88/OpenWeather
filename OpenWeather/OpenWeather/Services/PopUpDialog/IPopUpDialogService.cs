using System;
using OpenWeather.Models;

namespace OpenWeather.Services.PopUpDialog
{
    public interface IPopUpDialogService
    {
        void ShowMessage(WeatherMainModel weatherMainModel);
    }
}
