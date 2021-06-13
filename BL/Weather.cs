using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.AccWeatherRepository;
using Weather.Contract;
using Weather.Dal;
using Weather.Models;

namespace Weather.BL
{
    public class Weather : IWeather
    {
        private IAccWeather _accWeather;
        private AccWeatherContext _accWeatherContext;

        public Weather(IAccWeather accWeather, AccWeatherContext accWeatherContext)
        {
            _accWeather = accWeather;
            _accWeatherContext = accWeatherContext;
        }
        public async Task<LocationWeather> GetCurrentWeather(string cityKey, string cityName)
        {
            var weather = string.Empty;
            LocationData[] weatherData = null;
            LocationWeather weatherShort;

            try
            {
                weatherShort = _accWeatherContext.LocationWeathers
                     .FirstOrDefault(w => w.CityKey == cityKey);

                if (weatherShort != null && weatherShort.Date != DateTime.Today)
                    _accWeatherContext.LocationWeathers.Remove(weatherShort);

                if (weatherShort == null || weatherShort.Date != DateTime.Today)
                {
                    weatherData = await _accWeather.GetLocationWeather(cityKey);
                    weatherShort = new LocationWeather { Date = DateTime.Now, CityKey = cityKey, Temperature = Math.Round(weatherData[0].Temperature.Metric.Value, 2), WeatherText = weatherData[0].WeatherText, LocalizedName = cityName };
                    await _accWeatherContext.LocationWeathers.AddAsync(weatherShort).ConfigureAwait(false);
                    await _accWeatherContext.SaveChangesAsync().ConfigureAwait(false);
                }
                return weatherShort;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
