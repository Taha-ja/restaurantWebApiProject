using AutoMapper;
using DAL.Entities.DTOs;
using DAL.Entities.Models;

namespace RestaurantWebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>();
        }
    }
}
