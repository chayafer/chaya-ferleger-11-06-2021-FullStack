using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Contract;
using Weather.Models;

namespace Weather.BL
{
    public class Favorite : IFavorite
    {
        public async Task AddToFavorite(Models.City city)
        {
            try
            {
                using (var context = new AccWeatherContext())
                {
                    if (context.FavoritesCities.FirstOrDefault(w => w.CityKey == city.CityKey) == null)
                    {
                        context.FavoritesCities.Add(new FavoritesCity { CityKey = city.CityKey, LocalizedName = city.CityName });
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
           
           
        }

        public async Task DeleteFavorite(string cityKey)
        {
            try
            {
                using (var context = new AccWeatherContext())
                {
                    var city = context.FavoritesCities.FirstOrDefault(w => w.CityKey == cityKey);
                    if (city == null)
                        throw new ArgumentNullException("city is not existing in db");
                    context.FavoritesCities.Remove(city);
                    await context.SaveChangesAsync();
                }
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
                using (var context = new AccWeatherContext())
                {
                    return context.FavoritesCities.ToArray();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
