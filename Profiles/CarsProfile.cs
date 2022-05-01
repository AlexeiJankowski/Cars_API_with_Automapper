using AutoMapper;
using BMW_API.Dtos;

namespace BMW_API.Profiles
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            CreateMap<Car, ReadCarDto>();
            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();
            CreateMap<Car, UpdateCarDto>();
        }
    }
}