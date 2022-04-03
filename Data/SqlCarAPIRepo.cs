using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BMW_API
{
    public class SqlCarAPIRepo : ICarAPIRepo
    {
        private readonly AppDbContext _context;
        public SqlCarAPIRepo(AppDbContext context)
        {
             _context = context;            
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _context.Cars.ToList();
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

        public void UpdateCar(int id, Car newCar)
        {
            var carItem = _context.Cars.FirstOrDefault(x => x.Id == id);
            if(carItem != null)
            {
                carItem.VehicleClass = newCar.VehicleClass;
                carItem.Years = newCar.Years;
                carItem.ModelSeries = newCar.ModelSeries;
            } 
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