using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weather.BL;
using Weather.Contract;
using Weather.Middleware.ListsApi.Middleware;
using Weather.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private readonly ILogger<WeatherController> _logger;
        private ICity _city;
        private IFavorite _favorite;
        private IWeather _weather;

        public WeatherController(ILogger<WeatherController> logger, IFavorite favorite, ICity city, IWeather weather)
        {
            _logger = logger;
            _favorite = favorite;
            _city = city;
            _weather = weather;
        }

        [Route("/[controller]/v1/search")]
        [HttpGet]
        public async Task<ActionResult<Models.City[]>> SearchCities([FromQuery][Required] string key)
        {
            var cities = await _city.SearchCity(key);
            
            return new OkObjectResult(cities);

        }

        [Route("/[controller]/v1/currentweather")]
        [HttpGet]
        public async Task<ActionResult<LocationWeather>> GetCurrentWeather([FromQuery][Required] string cityKey, [FromQuery][Required] string cityName)
        {
            var weatherResult = await _weather.GetCurrentWeather(cityKey, cityName);
            
            return new OkObjectResult(weatherResult);

        }

        [Route("/[controller]/v1/addfavorite")]
        [HttpPut]
        public async Task<ActionResult<int>> AddToFavorite([FromBody][Required] Models.City city)
        {
            await _favorite.AddToFavorite(city);
            return new OkObjectResult(1);

        }

        [Route("/[controller]/v1/deletefavorite/{locationKey}")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteFavorite([FromRoute][Required] string locationKey)
        {
            await _favorite.DeleteFavorite(locationKey);
            return new OkObjectResult(1);

        }

        [Route("/[controller]/v1/getfavorites")]
        [HttpGet]
        public ActionResult<FavoritesCity[]> GetFavorite()
        {
            var favoritesCities = _favorite.GetFavorites();
            return new OkObjectResult(favoritesCities);

        }
    }
}
