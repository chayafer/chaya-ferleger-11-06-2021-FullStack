using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Contract
{
    public interface IWeather
    {
        Task<LocationWeather> GetCurrentWeather(string cityKey, string cityName);
    }
}
