using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenWeather.Models
{
//    public class WeatherMainModel
//    {
//        [JsonProperty("name")]
//        public string name { get; set; }
//        public WeatherTempDetails main { get; set; }
//        public List<WeatherSubDetails> weather { get; set; }
//        public WeatherWindDetails wind { get; set; }
//        public WeatherSysDetails sys { get; set; }
//    }

//    public class WeatherSubDetails
//    {
//        [JsonProperty("main")]
//        public string main { get; set; }
//        [JsonProperty("description")]
//        public string description { get; set; }
//        [JsonProperty("icon")]
//        public string icon { get; set; }
//    }

//    public class WeatherSysDetails
//    {
//        [JsonProperty("country")]
//        public string country { get; set; }
//    }

//    public class WeatherTempDetails
//    {
////        [JsonProperty("temp")]
    //    public string temp { get; set; }
    //    [JsonProperty("humidity")]
    //    public string humidity { get; set; }
    //}

    //public class WeatherWindDetails
    //{
    //    [JsonProperty("speed")]
    //    public string speed { get; set; }
    //}

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherMainModel
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}