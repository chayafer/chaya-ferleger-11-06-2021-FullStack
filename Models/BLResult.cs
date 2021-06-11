using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class BLResult<T>
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public T Response { get; set; }
    }
}
