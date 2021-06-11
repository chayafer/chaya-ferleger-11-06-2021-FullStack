using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.AccWeatherRepository
{
    public interface IAccWeather
    {
        public Task<Models.WeatherCity[]> GetCities(string key);
        public Task<LocationData[]> GetLocationWeather(string cityKey);
    }
}
