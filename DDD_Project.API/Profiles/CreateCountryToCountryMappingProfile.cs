using AutoMapper;
using DDD_Project.API.ViewModels;
using DDD_Project.Domain.Entities;

namespace DDD_Project.API.Profiles
{
    public class CreateCountryToCountryMappingProfile : Profile
    {
        public CreateCountryToCountryMappingProfile()
        {
            CreateMap<CreateCountryViewModel, Country>().ReverseMap()
                .ForMember(cc => cc.Name, c => c.MapFrom(src => src.Name));
        }
    }
}
