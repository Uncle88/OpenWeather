using System;
using System.Threading.Tasks;
using OpenWeather.Constants;
using OpenWeather.Models;
using OpenWeather.Services.Rest;

namespace OpenWeather.Services.DataWeather
{
    public class DataWeatherService : IDataWeatherService
    {
        public IRestService _restService;

        public DataWeatherService()
        {
            _restService = new RestService();
        }

        public async Task<Weather> GetWeather()
        {
            var results = await _restService.GetDataForecast(WeatherConstants.queryString).ConfigureAwait(false);
            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
