using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class City
    {
        [Required]
        public string CityKey { get; set; }
        
        [Required]
        public string CityName { get; set; }
       
    }
}
