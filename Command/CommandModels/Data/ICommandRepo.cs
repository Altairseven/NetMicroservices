using CommandModels.Models;


namespace CommandModels.Data;

public interface ICommandRepo {

    bool SaveChanges();

    //platforms
    IEnumerable<Platform> GetAllPlatforms();
    void CreatePlatform(Platform plat);
    bool PlatformExists(int PlatformId);
    bool ExternalPlatformExists(int ExternalPlatformId);

    //platforms
    IEnumerable<Command> GetCommandsForPlatform(int GetCommandsForPlatform);
    Command? GetCommand(int PlatformId, int commandId);
    void CreateCommand(int platformId, Command com);


}
