using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace BMW_API.Helpers
{
    public class SearchHelper
    {
        public static List<Car> OrderBy(List<Car> cars, string orderBy, bool desc)
        {
            if(String.IsNullOrEmpty(orderBy))
            {
                return cars.OrderBy(x => x.Id).ToList();
            }

            if(desc)
            {
                orderBy += " desc";                
            }  

            return cars.AsQueryable().OrderBy(orderBy).ToList();   
        }

        public static List<Car> Search(List<Car> cars, string searchBy)
        {
            var carsToReturn = new List<Car>();
            foreach (var carItem in cars)
            {
                foreach (var item in (carItem.GetType().GetProperties()))
                {
                    if (item.Name == "Id") continue;
                    if (Convert.ToString(item.GetValue(carItem)).ToLower().Contains(searchBy.ToLower()))
                    {
                        carsToReturn.Add(carItem);
                    }
                }
            }

            return carsToReturn;
        }
    }
}