using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BMW_API.Helpers;
using BMW_API.Parameters;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using static BMW_API.Parameters.CarsParameters;

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
            string searchBy = carsParameters.SearchQuery;
            string orderBy = carsParameters.OrderBy;
            bool desc = carsParameters.Desc;

            var cars = _context.Cars.ToList();

            cars = SearchHelper.OrderBy(cars, orderBy, desc);

            if (!String.IsNullOrEmpty(searchBy))
            {                
                cars = SearchHelper.Search(cars, searchBy);
            }

            var result = PaginatedList<Car>.Create(cars, carsParameters.CurrentPage, carsParameters.PageSize);

            if (result.Count <= 0)
            {
                return null;
            }

            return result;
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
            if (carItem == null)
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
            if (!_context.Cars.Any())
            {
                for (int i = 0; i < carList.Count; i += 3)
                {
                    _context.Cars.Add(new Car
                    {
                        ModelSeries = carList[i].InnerText,
                        Years = carList[i + 1].InnerText,
                        VehicleClass = carList[i + 2].InnerText.TrimEnd('\n')
                    });
                }
            }
        }
    }
}