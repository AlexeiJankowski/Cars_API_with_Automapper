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
        private int currentPage;
        public int CurrentPage 
        { 
            get => currentPage; 
            set => currentPage = value >= 1 ? value : 1; 
        }

        // OrderBy
        public string OrderBy { get; set; }

        // Search
        public string SearchQuery { get; set; }
    }
}