using AutomatedTransportEnquiry.Data;
using AutomatedTransportEnquiry.DTOs;
using AutomatedTransportEnquiry.Mapping;
using AutomatedTransportEnquiry.Models;
using AutomatedTransportEnquiry.Repositories;
using AutomatedTransportEnquiry.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IVehicleRouteRepository, VehicleRouteRepository>();



builder.Services.AddScoped<IVehicleRouteService, VehicleRouteService>();



//Registering AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

//For Vehicle

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

//For Schedules 
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

//For Search
builder.Services.AddScoped<ISearchRepository,SearchRepository>();
builder.Services.AddScoped<ISearchService,SearchService>();

//For Fares
builder.Services.AddScoped<IFareService, FareService>();
builder.Services.AddScoped<IFareRepository, FareRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
