using CommandModels.Data;
using CommandModels.Models;


namespace CommandService.Repositories;

public class CommandRepository : ICommandRepo {
    private readonly CommandDbContext _db;

    public CommandRepository(CommandDbContext db) {
        _db = db;
    }


    public void CreateCommand(int platformId, Command com) {
        if (com == null) {
            throw new ArgumentNullException(nameof(com));
        }
       
        com.PlatformId = platformId;

        _db.Commands.Add(com);
    }

    public void CreatePlatform(Platform plat) {
        if(plat == null) {
            throw new ArgumentNullException(nameof(plat));
        }

        _db.Platforms.Add(plat);
    }

    public bool ExternalPlatformExists(int ExternalPlatformId) {
        return _db.Platforms.Any(x=> x.ExternalID == ExternalPlatformId);
    }

    public IEnumerable<Platform> GetAllPlatforms() {
        return _db.Platforms.ToList();
    }

    public Command? GetCommand(int PlatformId, int commandId) {
        return _db.Commands.Where(x => x.PlatformId == PlatformId && x.Id == commandId).FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId) {
        return _db.Commands.Where(x => x.PlatformId == platformId)
            .OrderBy(x=> x.Platform!.Name).ToList();
    }

    public bool PlatformExists(int PlatformId) {
        return _db.Platforms.Any(p => p.Id == PlatformId);
        
    }

    public bool SaveChanges() {
        return _db.SaveChanges() >= 0;
    }
}
