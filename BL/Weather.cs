using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.AccWeatherRepository;
using Weather.Contract;
using Weather.Models;

namespace Weather.BL
{
    public class Weather : IWeather
    {
        private IAccWeather _accWeather;
        public Weather(IAccWeather accWeather)
        {
            _accWeather = accWeather;
        }
        public async Task<LocationWeather> GetCurrentWeather(string cityKey, string cityName)
        {
            var weather = string.Empty;
            LocationData[] weatherData = null;
            LocationWeather weatherShort;

            try
            {
                using (var context = new AccWeatherContext())
                {
                    weatherShort = context.LocationWeathers
                        .FirstOrDefault(w => w.CityKey == cityKey);

                    if (weatherShort != null && weatherShort.Date != DateTime.Today)
                    {
                        context.LocationWeathers.Remove(weatherShort);
                    }

                    if (weatherShort == null || weatherShort.Date != DateTime.Today)
                    {
                        weatherData = await _accWeather.GetLocationWeather(cityKey);
                        weatherShort = new LocationWeather { Date = DateTime.Now, CityKey = cityKey, Temperature = Math.Round(weatherData[0].Temperature.Metric.Value, 2), WeatherText = weatherData[0].WeatherText, LocalizedName = cityName };
                        await context.LocationWeathers.AddAsync(weatherShort).ConfigureAwait(false);
                        await context.SaveChangesAsync().ConfigureAwait(false);
                    }
                    return weatherShort;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
       
    }
}
