using System;
using System.Collections.Generic;
using OpenWeather.ViewModels;
using Xamarin.Forms;

namespace OpenWeather.Views
{
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            BindingContext = new MenuViewModel();
            InitializeComponent();
        }
    }
}
