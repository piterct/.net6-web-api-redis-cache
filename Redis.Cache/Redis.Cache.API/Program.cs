using Microsoft.EntityFrameworkCore;
using Redis.Cache.Application.Inrterfaces.Repositories;
using Redis.Cache.Application.Inrterfaces.Repositories.Cache;
using Redis.Cache.Infra.DbContexts;
using Redis.Cache.Infra.Repositories;
using Redis.Cache.Infra.Repositories.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddDbContext<LikeDbContext>(o => o.UseInMemoryDatabase("LikeDb"));
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICacheRepository, CacheRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();
