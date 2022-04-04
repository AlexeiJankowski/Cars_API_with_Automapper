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
        public ActionResult<ReadCarDto> GetCarById(int id) 
        {
            var carItem = _repository.GetCarById(id);
            if(carItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReadCarDto>(carItem));
        }

        [HttpPost]
        public ActionResult<ReadCarDto> CreateCar(CreateCarDto createCarDto)
        {
            var carItem = _mapper.Map<Car>(createCarDto);
            _repository.CreateNewCar(carItem);
            _repository.SaveChanges();

            var carReadDto = _mapper.Map<ReadCarDto>(carItem);

            return CreatedAtRoute(nameof(GetCarById), new { Id = carReadDto.Id }, carReadDto);
        } 

        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, UpdateCarDto updateCarDto)
        {
            var carItem = _repository.GetCarById(id);
            if(carItem == null)
            {
                return NotFound();
            }            
            
            _mapper.Map(updateCarDto, carItem);
            _repository.SaveChanges();

            return NoContent();
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