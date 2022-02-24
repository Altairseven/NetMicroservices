using AutoMapper;
using Grpc.Core;
using PlatformModels.Data;
using PlatformModels.Models;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase {

    private readonly IRepository<Platform> _repo;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IRepository<Platform> repo, IMapper mapper) {
        _repo = repo;
        _mapper = mapper;
    }

    public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context) { 
        var response = new PlatformResponse();
        var platforms = _repo.GetAll();

        platforms.ToList()
            .ForEach(platform => 
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform))
        );

        return Task.FromResult(response);
    }


}
