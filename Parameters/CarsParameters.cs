using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMW_API.Parameters
{
    public class CarsParameters
    {
        // Pagination
        const int maxPageSize = 20;
        const int standardPageSize = 10;
        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = ((value > maxPageSize) || (value <= 0)) ? value = standardPageSize : value;
        }
        public int CurrentPage { get; set; } = 1;

        // OrderBy
        public string OrderBy { get; set; } = "Name";
    }
}