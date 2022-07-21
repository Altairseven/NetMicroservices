using CommandModels.Data;
using CommandModels.Models;
using CommandService.SyncDataService.Grpc;

namespace CommandService.Data;

public static class PrepDb {

    public static void PrepDbPopulation(this IApplicationBuilder applicationBuilder) {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var grpcClient = serviceScope!.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient!.ReturnAllPlatforms();

        SeedData(serviceScope!.ServiceProvider.GetService<ICommandRepo>()!, platforms);
    }

    private static void SeedData(ICommandRepo _repo, IEnumerable<Platform> platforms) {
        Console.WriteLine("Seeding new platforms...");
        platforms.ToList().ForEach(plat => {
            if (!_repo.ExternalPlatformExists(plat.ExternalID)) _repo.CreatePlatform(plat);
        });
        _repo.SaveChanges();
    }

}
