using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandModels.Models;
using CommandModels.Dtos;

namespace CommandModels.Mappers;

public class CommandModelsProfile :Profile {

    public CommandModelsProfile() {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<Command, CommandReadDto>();

        CreateMap<PlatformPublishDto, Platform>()
            .ForMember(dst=> dst.ExternalID, 
                       opt=> opt.MapFrom(src=> src.Id));

        //CreateMap<GrpcPlatformModel, Platform>();
    }

}
