using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Contract
{
    public interface IFavorite
    {
        Task AddToFavorite(City city);
        Task DeleteFavorite(string cityKey);
        FavoritesCity[] GetFavorites();
    }
}
