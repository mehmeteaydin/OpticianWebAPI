using OpticianWebAPI;
using OpticianWebAPI.Services;
using OpticianWebAPI.Services.concretes;
using Microsoft.EntityFrameworkCore;
using OpticianWebAPI.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

// ARTIK HARDCODE YOK, appsettings'den okumalı:
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFrameService,FrameService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
