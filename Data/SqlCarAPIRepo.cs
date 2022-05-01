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
            return PaginatedList<Car>.Create(_context.Cars.OrderBy(x => x.Id), carsParameters.CurrentPage, carsParameters.PageSize);        
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