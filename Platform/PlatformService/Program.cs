using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PlatformModels.Data;
using PlatformModels.Mappers;
using PlatformModels.Models;
using PlatformService.AsyncDataServices;
using PlatformService.Mappers;
using PlatformService.Repositories;
using PlatformService.SyncDataService.Http;
using PlatformService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

#region Services

if (builder.Environment.IsProduction()){
    builder.Services.AddDbContext<PlatformDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConnection"), 
        b=> b.MigrationsAssembly("PlatformService"))
    );
    Console.WriteLine("--> Using MS SQL Server");
}

else {
    builder.Services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase("inMem"));
    Console.WriteLine("--> Using InMemory DB");
}

builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PlatformModelsProfile));
builder.Services.AddAutoMapper(typeof(PlatformServiceProfile));
builder.Services.AddGrpc();

Console.WriteLine($"CommandServiceEndpoint => {builder.Configuration["CommandService"]}");

#endregion;

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}


app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcPlatformService>();

    endpoints.MapGet("/protos/platforms.proto", async context => {
        await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
    });
});


app.FeedDb(app.Environment.IsProduction());


#endregion

app.Run();
