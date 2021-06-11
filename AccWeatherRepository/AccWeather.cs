using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.AccWeatherRepository
{
    public class AccWeather : IAccWeather
    {
        private IConfiguration _configuration;
        private string _accWeatherApiKey;

        public AccWeather(IConfiguration configuration)
        {
            _configuration = configuration;
            _accWeatherApiKey = configuration.GetSection("AccWeatherApiData")?["Apikey"];
        }
        public async Task<Models.WeatherCity[]> GetCities(string key)
        {
            WeatherCity[] citiesApi = null;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var uri = _configuration.GetSection("AccWeatherApiData")?["CitiesUri"];
                    using (var resp = await httpClient.GetAsync($"{uri}={_accWeatherApiKey}&q={key}"))
                    {
                        if (!resp.IsSuccessStatusCode)
                            throw new HttpRequestException($"An error occurred, got status code from accuweather api {(int)resp.StatusCode}");

                        var citiesString = await resp.Content?.ReadAsStringAsync();
                        if (citiesString == null)
                            throw new ArgumentNullException();

                        citiesApi = JsonConvert.DeserializeObject<WeatherCity[]>(citiesString);
                        if (citiesApi == null || citiesApi.Length == 0)
                            throw new ArgumentNullException();

                    }

                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (NotSupportedException ex)
                {
                    throw new NotSupportedException($"The content type is not supported. {ex.Message}", ex);
                }
                catch (JsonException ex)
                {
                    throw new JsonException("Invalid JSON got from accApi", ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return citiesApi;

            }
        }
        public async Task<LocationData[]> GetLocationWeather(string cityKey)
        {
            LocationData[] weatherData;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var uri = _configuration.GetSection("AccWeatherApiData")?["LocationUri"];
                    
                    using (var resp = await httpClient.GetAsync($"{uri}{cityKey}?apikey={_accWeatherApiKey}"))
                    {
                        if (!resp.IsSuccessStatusCode)
                            throw new HttpRequestException($"An error occurred, got status code from accuweather api {(int)resp.StatusCode}");

                        var weatherString = await resp.Content?.ReadAsStringAsync();
                        if (weatherString == null)
                            throw new ArgumentNullException();

                        weatherData = JsonConvert.DeserializeObject<LocationData[]>(weatherString);
                        if (weatherData == null || weatherData.Length == 0)
                            throw new ArgumentNullException();
                    }

                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (NotSupportedException ex)
                {
                    throw new NotSupportedException($"The content type is not supported. {ex.Message}", ex);
                }
                catch (JsonException ex)
                {
                    throw new JsonException("Invalid JSON got from accApi", ex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return weatherData;
        }


    }
}
