using Microsoft.EntityFrameworkCore;
using PlatformModels.Data;
using PlatformModels.Mappers;
using PlatformModels.Models;
using PlatformService.Repositories;


var builder = WebApplication.CreateBuilder(args);


#region Services

builder.Services.AddDbContext<PlatformDbContext>(opt => opt.UseInMemoryDatabase("inMem"));

builder.Services.AddScoped<IRepository<Platform>, PlatformRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PlatformProfile));
#endregion;

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.FeedDb();


#endregion

app.Run();
