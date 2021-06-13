using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Contract;
using Weather.Dal;
using Weather.Models;

namespace Weather.BL
{
    public class Favorite : IFavorite
    {
        private AccWeatherContext _accWeatherContext;

        public Favorite(AccWeatherContext accWeatherContext)
        {
            _accWeatherContext = accWeatherContext;
        }
        public async Task AddToFavorite(Models.City city)
        {
            try
            {
                if (_accWeatherContext.FavoritesCities.FirstOrDefault(w => w.CityKey == city.CityKey) == null)
                {
                    _accWeatherContext.FavoritesCities.Add(new FavoritesCity { CityKey = city.CityKey, LocalizedName = city.CityName });
                    await _accWeatherContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task DeleteFavorite(string cityKey)
        {
            try
            {
                var city = _accWeatherContext.FavoritesCities.FirstOrDefault(w => w.CityKey == cityKey);
                if (city == null)
                    throw new ArgumentNullException("city is not existing in db");
                _accWeatherContext.FavoritesCities.Remove(city);
                await _accWeatherContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public FavoritesCity[] GetFavorites()
        {
            try
            {
                return _accWeatherContext.FavoritesCities.ToArray();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
