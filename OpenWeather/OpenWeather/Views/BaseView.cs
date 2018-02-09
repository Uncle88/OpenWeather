using System;
using OpenWeather.ViewModels;
using Xamarin.Forms;

namespace OpenWeather.Views
{
    public class BaseView : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null && BindingContext is ViewModelBase)
            {
                ((ViewModelBase)BindingContext).Initialize();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext != null && BindingContext is ViewModelBase)
            {
                ((ViewModelBase)BindingContext).Deinitialize();
            }
        }
    }
}
