using AutoMapper;
using PlatformModels.Models;

namespace PlatformService.Mappers {
    public class PlatformServiceProfile : Profile {

        public PlatformServiceProfile() {
           
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest=> dest.PlatformId, 
                           opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => src.Name)
                )
                .ForMember(dest => dest.Publisher,
                           opt => opt.MapFrom(src => src.Publisher)
                );
        }
    }
}
