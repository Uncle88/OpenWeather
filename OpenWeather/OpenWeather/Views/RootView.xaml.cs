using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace OpenWeather.Views
{
    public partial class RootView : MasterDetailPage
    {
        public RootView()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
        }
    }
}
