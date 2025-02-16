using AutoMapper;
using DDD_Project.API.ViewModels;
using DDD_Project.Domain.Entities;

namespace DDD_Project.API.Profiles
{
    public class EditPhoneToPhoneMappingProfile : Profile
    {
        public EditPhoneToPhoneMappingProfile()
        {
            CreateMap<EditPhoneViewModel, Phone>().ReverseMap()
                .ForMember(cp => cp.Name, p => p.MapFrom(src => src.Name))
                .ForMember(cp => cp.Year, p => p.MapFrom(src => src.Year))
                .ForMember(cp => cp.CountryId, p => p.MapFrom(src => src.CountryId));
        }
    }
}
