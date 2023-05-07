using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;
using Redis.Cache.Application.Inrterfaces.Repositories.Fakes;
using Redis.Cache.Application.Inrterfaces.Services;
using Redis.Cache.Infra.DbContexts;
using Redis.Cache.Infra.Repositories;
using Redis.Cache.Infra.Repositories.Cache;
using Redis.Cache.Infra.Repositories.Fakes;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
    .AddEnvironmentVariables();

IConfiguration configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddDbContext<LikeDbContext>(o => o.UseInMemoryDatabase("LikeDb"));

//Using LocalHost WebApi Redis

builder.Services.AddStackExchangeRedisCache(o =>
{
    o.InstanceName = configuration["ConfigRedis:InstanceName"];
    o.Configuration = configuration["ConfigRedis:ConfigurationInstance"];
});


// Services
builder.Services.AddScoped<ILikeService, LikeService>();

// Repositories
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICacheRepository, CacheRepository>();
builder.Services.AddScoped<IFakeLikeRepository, FakeLikeRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapControllers();


app.Run();
