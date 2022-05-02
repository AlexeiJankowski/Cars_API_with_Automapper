using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BMW_API.Helpers;
using BMW_API.Parameters;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BMW_API.Data
{
    public class SqlCarAPIRepo : ICarAPIRepo
    {
        private readonly AppDbContext _context;
        public SqlCarAPIRepo(AppDbContext context)
        {
             _context = context;            
        }

        public PaginatedList<Car> GetAllCars(CarsParameters carsParameters)
        {
            var carItems = _context.Cars.ToList();

            switch(carsParameters.OrderBy)
            {
                case "modelSeries":
                    carItems = carItems.OrderBy(x => x.ModelSeries).ToList();
                    break;
                case "years":
                    carItems = carItems.OrderBy(x => x.Years).ToList();
                    break;
                case "class":
                    carItems = carItems.OrderBy(x => x.VehicleClass).ToList();
                    break;
                default:
                    carItems = carItems.OrderBy(x => x.Id).ToList();
                    break;
            }

            if(!String.IsNullOrEmpty(carsParameters.SearchQuery))
            {
                var carItemsToReturn = new List<Car>();
                foreach(var carItem in carItems)
                {
                    foreach(var item in (carItem.GetType().GetProperties()))
                    {
                        if(item.Name == "Id") continue;
                        if(Convert.ToString(item.GetValue(carItem)).ToLower().Contains(carsParameters.SearchQuery.ToLower()))
                        {
                            carItemsToReturn.Add(carItem);
                        }
                    }
                }
                carItems = carItemsToReturn;
            }

            return PaginatedList<Car>.Create(carItems, carsParameters.CurrentPage, carsParameters.PageSize);        
        }

        public Car GetCarById(int id)
        {
            return _context.Cars.FirstOrDefault(x => x.Id == id);
        }

        public Car CreateNewCar(Car car)
        {
            _context.Cars.Add(car);
            return car;
        }    

        public void UpdateCar(int id)
        {
            
        }   

        public void DeleteCar(int id)
        {
            var carItem = _context.Cars.FirstOrDefault(x => x.Id == id);
            if(carItem == null)
            {
                throw new ArgumentNullException(nameof(carItem));
            }
            _context.Cars.Remove(carItem);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Seed Method

        public void PostCarListToDb(HtmlNodeCollection carList)
        {
            if(!_context.Cars.Any())
            {
                for(int i = 0; i < carList.Count; i += 3)
                {   
                    _context.Cars.Add(new Car 
                    {
                        ModelSeries = carList[i].InnerText,
                        Years = carList[i + 1].InnerText,
                        VehicleClass = carList[i + 2].InnerText
                    });
                }  
            }                      
        }
    }
}