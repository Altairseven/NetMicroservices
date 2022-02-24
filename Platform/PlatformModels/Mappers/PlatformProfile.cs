using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlatformModels.Dtos;
using PlatformModels.Models;

namespace PlatformModels.Mappers {
    public class PlatformModelsProfile : Profile {

        public PlatformModelsProfile() {
            CreateMap<Platform, PlatformDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformDto, PlatformPublishDto>();
        }
    }
}
