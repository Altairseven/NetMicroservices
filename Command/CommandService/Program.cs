using CommandModels.Data;
using CommandModels.Mappers;
using CommandService.AsyncDataServices;
using CommandService.EventProcessing;
using CommandService.Mappers;
using CommandService.Repositories;
using CommandService.SyncDataService.Grpc;
using Microsoft.EntityFrameworkCore;
using CommandService.Data;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddDbContext<CommandDbContext>(Opt=> Opt.UseInMemoryDatabase("inMem"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();


builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddScoped<ICommandRepo, CommandRepository>();
builder.Services.AddAutoMapper(typeof(CommandModelsProfile));
builder.Services.AddAutoMapper(typeof(CommandServiceProfile));

#endregion

var app = builder.Build();

#region Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();*/

app.UseAuthorization();

app.MapControllers();

app.PrepDbPopulation();

#endregion

app.Run();
