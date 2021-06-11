using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Contract
{
    public interface ICity
    {
        public Task<City[]> SearchCity(string key);
    }
}
