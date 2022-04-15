using System.Collections.Generic;
using HtmlAgilityPack;

namespace BMW_API
{
    public interface ICarAPIRepo
    {
        public void PostCarListToDb(HtmlNodeCollection carList);
        public IEnumerable<Car> GetAllCars();
        public Car GetCarById(int id);
        public Car CreateNewCar(Car car);
        public void UpdateCar(int id);
        public void DeleteCar(int id);
        public void SaveChanges();
    }
}