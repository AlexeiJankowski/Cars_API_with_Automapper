using AutoMapper;

namespace BMW_API
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