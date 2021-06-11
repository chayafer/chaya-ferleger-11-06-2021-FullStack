using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.BL
{
    public class WeatherLogic
    {

        //public async static Task<BLResult<City[]>> SearchCity(string key)
        //{
        //    var weather = string.Empty;
        //    WeatherCity[] citiesApi = null;
        //    Models.City[] cities;

        //    using (var httpClient = new HttpClient())
        //    {
        //        try
        //        {
        //            using (var resp = await httpClient.GetAsync($"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=rLF5Q34EVFdmsffh2ZGOan2cvea7okba&q={key}"))
        //            {
        //                if (!resp.IsSuccessStatusCode)
        //                    return new BLResult<City[]> { Error = $"An error occurred, got status code from accuweather api", StatusCode = (int)resp.StatusCode };

        //                var citiesString = await resp.Content?.ReadAsStringAsync();
        //                if (citiesString == null)
        //                    return new BLResult<City[]> { Error = $"An error occurred response from api is null.", StatusCode = StatusCodes.Status500publicServerError };

        //                citiesApi = JsonConvert.DeserializeObject<WeatherCity[]>(citiesString);
        //                if (citiesApi == null || citiesApi.Length == 0)
        //                    return new BLResult<City[]> { Error = $"An error occurred, could not deserilie response.", StatusCode = StatusCodes.Status500publicServerError };

        //            }

        //        }
        //        catch (HttpRequestException ex) // Non success
        //        {
        //            return new BLResult<City[]> { Error = $"An error occurred. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //        }
        //        catch (NotSupportedException ex) // When content type is not valid
        //        {
        //            return new BLResult<City[]> { Error = $"The content type is not supported. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //        }
        //        catch (JsonException ex) // Invalid JSON
        //        {
        //            return new BLResult<City[]> { Error = $"Invalid JSON. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //        }
        //        catch (Exception ex) // Invalid JSON
        //        {
        //            return new BLResult<City[]> { Error = $"An error occurred. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //        }
        //        cities = citiesApi.Select(a => new Models.City { CityKey = a.Key, CityName = a.LocalizedName }).ToArray();

        //        return new BLResult<City[]> { Response = cities, StatusCode = StatusCodes.Status200OK };

        //    }


        //}
        //public async static Task<BLResult<LocationWeather>> GetCurrentWeather(string cityKey,string cityName)
        //{

        //    var weather = string.Empty;
        //    LocationData[] weatherData = null;
        //    LocationWeather weatherShort;
        //    using (var context = new AccWeatherContext())
        //    {
        //        weatherShort = context.LocationWeathers.FirstOrDefault(w => w.CityKey == cityKey);
        //        if (weatherShort != null)
        //        {
        //            if (weatherShort.Date != DateTime.Today)
        //                context.LocationWeathers.Remove(weatherShort);

        //        }
        //        if (weatherShort == null || weatherShort.Date != DateTime.Today)
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                try
        //                {
        //                    using (var resp = await httpClient.GetAsync($"http://dataservice.accuweather.com/currentconditions/v1/{cityKey}?apikey=rLF5Q34EVFdmsffh2ZGOan2cvea7okba"))
        //                    {
        //                        if (!resp.IsSuccessStatusCode)
        //                            return new BLResult<LocationWeather> { Error = $"An error occurred, got status code from accuweather api", StatusCode = (int)resp.StatusCode };

        //                        var weatherString = await resp.Content?.ReadAsStringAsync();
        //                        if (weatherString == null)
        //                            return new BLResult<LocationWeather> { Error = $"An error occurred response from api is null.", StatusCode = StatusCodes.Status500publicServerError };

        //                        weatherData = JsonConvert.DeserializeObject<LocationData[]>(weatherString);
        //                        if (weatherData == null || weatherData.Length == 0)
        //                            return new BLResult<LocationWeather> { Error = $"An error occurred, could not deserilie response.", StatusCode = StatusCodes.Status500publicServerError };
        //                    }

        //                }
        //                catch (HttpRequestException ex) // Non success
        //                {
        //                    return new BLResult<LocationWeather> { Error = $"An error occurred. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //                }
        //                catch (NotSupportedException ex) // When content type is not valid
        //                {
        //                    return new BLResult<LocationWeather> { Error = $"The content type is not supported. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //                }
        //                catch (JsonException ex) // Invalid JSON
        //                {
        //                    return new BLResult<LocationWeather> { Error = $"Invalid JSON. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //                }
        //                catch (Exception ex) // Invalid JSON
        //                {
        //                    return new BLResult<LocationWeather> { Error = $"An error occurred. {ex.Message}", StatusCode = StatusCodes.Status500publicServerError };
        //                }

        //            }
        //            weatherShort = new LocationWeather { Date = DateTime.Now, CityKey = cityKey, Temperature = Math.Round(weatherData[0].Temperature.Metric.Value,2), WeatherText = weatherData[0].WeatherText, LocalizedName= cityName};
        //            await context.LocationWeathers.AddAsync(weatherShort).ConfigureAwait(false);
        //            await context.SaveChangesAsync().ConfigureAwait(false);
        //        }
        //        return new BLResult<LocationWeather> { Response = weatherShort, StatusCode = StatusCodes.Status200OK };

        //    }


        //}

        //public async static Task<BLResult<int>> AddToFavorite(City city)
        //{
        //    using (var context = new AccWeatherContext())
        //    {
        //        if (context.FavoritesCities.FirstOrDefault(w => w.CityKey == city.CityKey) != null)
        //            return new BLResult<int>() { Error = "city is existing in favorites", StatusCode = StatusCodes.Status200OK };
        //        var cityName = context.LocationWeathers.FirstOrDefault(l => l.CityKey == city.CityKey)?.LocalizedName;
        //        context.FavoritesCities.Add(new favoritesCity { CityKey = city.CityKey, LocalizedName = city.CityName });
        //        await context.SaveChangesAsync();
        //    }
        //    return new BLResult<int>() { StatusCode = StatusCodes.Status200OK , Response=1};
        //}

        //public static async Task<BLResult<int>> DeleteFavorite(string cityKey)
        //{
        //    using (var context = new AccWeatherContext())
        //    {
        //        var city = context.FavoritesCities.FirstOrDefault(w => w.CityKey == cityKey);
        //        if (city == null)
        //            return new BLResult<int>() { Error = "city is not existing in db", StatusCode = StatusCodes.Status400BadRequest };
        //        context.FavoritesCities.Remove(city);
        //        await context.SaveChangesAsync();
        //    }
        //    return new BLResult<int>() { StatusCode = StatusCodes.Status200OK,Response=1 };
        //}
        //public static BLResult<favoritesCity[]> GetFavorites()
        //{
        //    using (var context = new AccWeatherContext())
        //    {
        //        var list = context.FavoritesCities.ToArray();
        //        if (list == null || list.Length == 0)
        //            return new BLResult<favoritesCity[]>() { Error = "favorites tables is empty", StatusCode = StatusCodes.Status204NoContent };
        //        return new BLResult<favoritesCity[]>() { Response = list, StatusCode = StatusCodes.Status200OK };
        //    }

        //}

    }
}
