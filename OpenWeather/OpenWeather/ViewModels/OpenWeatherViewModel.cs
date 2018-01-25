﻿using System;
using System.Threading.Tasks;
using OpenWeather.Models;
using OpenWeather.Services.DataWeather;
using OpenWeather.Services.LocalStorage;
using OpenWeather.Services.Rest;

namespace OpenWeather.ViewModels
{
    public class OpenWeatherViewModel : ViewModelBase
    {
        private readonly IDataWeatherService _dataWeatherService;
        private readonly IRestService _restService;
        private readonly ILocalStorageService _localStorageService = null;

        public OpenWeatherViewModel()
        {
            _dataWeatherService = new DataWeatherService();
            _restService = new RestService();
            _localStorageService = new LocalStorageService();

            ReadFromPCLStorage();
        }

        private WeatherMainModel _weatherMainModel; 
        public WeatherMainModel WeatherMainModel
        {
            get { return _weatherMainModel; }
            set
            {
                _weatherMainModel = value;
                IconImageString = "http://openweathermap.org/img/w/" + _weatherMainModel.weather[0].icon + ".png";
                OnPropertyChanged();
            }
        }

        private string _iconImageString;
        public string IconImageString
        {
            get { return _iconImageString; }
            set
            {
                _iconImageString = value;
                OnPropertyChanged();
            }
        }
    
        private string _city; 
        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                {
                    if (value != null)
                    {
                        _city = value;
                        Task.Run(async () =>
                        {
                            await InitializeGetWeatherAsync();
                        });
                        OnPropertyChanged();
                    }
                }
            }
        }

        private async Task ReadFromPCLStorage()
        {
            WeatherMainModel = await _localStorageService.PCLReadStorage<WeatherMainModel>();
        }

        private async Task InitializeGetWeatherAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(1000);
                WeatherMainModel = await _dataWeatherService.GetWeatherByCityName(_city);
                WriteToPCLStorage();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void WriteToPCLStorage()
        {
            if (_localStorageService.Equals(null))
            {
                _localStorageService.PCLWriteStorage(WeatherMainModel);
            }
            else
            {
                _localStorageService.ClearStorage();
                _localStorageService.PCLWriteStorage(WeatherMainModel);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}
