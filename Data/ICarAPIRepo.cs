using System.Collections.Generic;
using BMW_API.Helpers;
using HtmlAgilityPack;

namespace BMW_API.Data
{
    public interface ICarAPIRepo
    {
        public void PostCarListToDb(HtmlNodeCollection carList);
        public PaginatedList<Car> GetAllCars();
        public Car GetCarById(int id);
        public Car CreateNewCar(Car car);
        public void UpdateCar(int id);
        public void DeleteCar(int id);
        public void SaveChanges();
    }
}