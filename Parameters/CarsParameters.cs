using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMW_API.Parameters
{
    public class CarsParameters
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; } = 1;
    }
}