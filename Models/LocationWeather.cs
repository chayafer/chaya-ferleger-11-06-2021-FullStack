using System;
using System.Collections.Generic;

#nullable disable

namespace Weather.Models
{
    public partial class LocationWeather
    {
        public string CityKey { get; set; }
        public string LocalizedName { get; set; }
        public DateTime? Date { get; set; }
        public double? Temperature { get; set; }
        public string WeatherText { get; set; }
    }
}
