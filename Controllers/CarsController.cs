using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BMW_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarAPIRepo _repository;
        private readonly IMapper _mapper;
        
        public CarsController(ICarAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadCarDto>> GetAllCars() 
        {        
            var carItems = _repository.GetAllCars();
            return Ok(_mapper.Map<IEnumerable<ReadCarDto>>(carItems));
        }

        [HttpGet("{id}", Name = "GetCarById")]
        public ActionResult<Car> GetCarById(int id) 
        {
            var carItem = _repository.GetCarById(id);
            if(carItem == null)
            {
                return NotFound();
            }
            return Ok(carItem);
        }

        [HttpPost]
        public ActionResult<Car> CreateCar(Car car)
        {
            var carItem = _repository.CreateNewCar(car);
            if(carItem == null)
            {
                return NotFound();
            }
            _repository.SaveChanges();
            return Ok(carItem);
        } 

        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, Car car)
        {
            var carItem = _repository.GetCarById(id);
            if(carItem == null)
            {
                return NotFound();
            }            
            _repository.UpdateCar(id, car);
            _repository.SaveChanges();
            return Ok(carItem);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var carItem = _repository.GetCarById(id);
            if(carItem == null)
            {
                return NotFound();
            }
            _repository.DeleteCar(id);
            _repository.SaveChanges();
            
            return NoContent();
        }
    }
}