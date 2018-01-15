using System;
namespace OpenWeather.Constants
{
    public class WeatherConstants
    {
        public static readonly string key = "2e1694f1042f59269ae0ae384121b511";

        public static readonly string queryString = "http://api.openweathermap.org/data/2.5/weather?lat=48&lon=34" + "&appid=" + key;

        public static readonly string OpenWeatherApi = "http://api.openweathermap.org/data/2.5/weather?q=";
    }
}
