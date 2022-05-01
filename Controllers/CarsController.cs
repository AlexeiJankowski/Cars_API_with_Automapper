using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using BMW_API.Data;
using BMW_API.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BMW_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarAPIRepo _repository;
        private readonly IMapper _mapper;
        
        public CarsController(ICarAPIRepo repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            _repository.UpdateCar(id);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartiallyUpdaCar(int id, JsonPatchDocument<UpdateCarDto> patchCar)
        {
            var carItem = _repository.GetCarById(id);
            
            if(carItem == null)
            {
                var carDto = new UpdateCarDto();
                patchCar.ApplyTo(carDto, ModelState);

                var carToAdd = _mapper.Map<Car>(carDto);
                carToAdd.Id = id;

                _repository.CreateNewCar(carToAdd);
                _repository.SaveChanges();

                var carToReturn = _mapper.Map<Car>(carToAdd);

                return CreatedAtRoute(nameof(GetCarById), new { Id = carToReturn.Id }, carToReturn);
            }

            var carToPatch = _mapper.Map<UpdateCarDto>(carItem);
            patchCar.ApplyTo(carToPatch, ModelState);

            _mapper.Map(carToPatch, carItem);
            
            _repository.UpdateCar(id);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetCarById), new { id }, carToPatch);
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