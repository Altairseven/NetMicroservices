using CommandModels.Models;

namespace CommandService.SyncDataService.Grpc;

public interface IPlatformDataClient {

    IEnumerable<Platform> ReturnAllPlatforms();
}
