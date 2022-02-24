using AutoMapper;
using CommandModels.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataService.Grpc;

public class PlatformDataClient : IPlatformDataClient {

    private readonly IConfiguration _conf;
    private readonly IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper) {
        _conf = configuration;
        _mapper = mapper;
    }



    public IEnumerable<Platform> ReturnAllPlatforms() {
        Console.WriteLine($"--> Calling GRPC Service {_conf["GrpcPlatform"]}");
        var channel = GrpcChannel.ForAddress(_conf["GrpcPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try {
            var reply = client.GetAllPlatforms(request);
            return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch (Exception ex) {
            Console.WriteLine($"--> Could not call GRPC Server - {ex.Message}");
            return Enumerable.Empty<Platform>();
        }
    }


}
