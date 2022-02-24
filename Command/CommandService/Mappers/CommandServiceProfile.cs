using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandModels.Models;
using CommandModels.Dtos;
using PlatformService;

namespace CommandService.Mappers;

public class CommandServiceProfile :Profile {

    public CommandServiceProfile() {

        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }

}
