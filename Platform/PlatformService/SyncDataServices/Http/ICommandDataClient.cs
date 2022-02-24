using PlatformModels.Dtos;

namespace PlatformService.SyncDataService.Http;

public interface ICommandDataClient {
    Task SendPlatformToCommand(PlatformDto platform);
}
