using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.AccWeatherRepository;
using Weather.Contract;
using Weather.Models;

namespace Weather.BL
{
    public class City : ICity
    {
        private IAccWeather _accWeather;
        public City(IAccWeather accWeather)
        {
            _accWeather = accWeather;
        }
        
        public async Task<Models.City[]> SearchCity(string key)
        {
            var weather = string.Empty;
            var citiesApi = await _accWeather.GetCities(key); ;

            return citiesApi.Select(a => new Models.City { CityKey = a.Key, CityName = a.LocalizedName }).ToArray();

        }
    }
}
