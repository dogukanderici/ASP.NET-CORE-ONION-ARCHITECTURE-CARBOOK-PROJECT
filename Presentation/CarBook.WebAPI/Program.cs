using CarBook.Application;
using CarBook.Application.Interfaces;
using CarBook.Application.Interfaces.CarInterfaces;
using CarBook.Application.Interfaces.RentACarInterfaces;
using CarBook.Application.Interfaces.StatisticsInterfaces;
using CarBook.Application.Services;
using CarBook.Persistance.Context;
using CarBook.Persistance.Repositories;
using CarBook.Persistance.Repositories.CarRepositories;
using CarBook.Persistance.Repositories.RentACarRepositories;
using CarBook.Persistance.Repositories.StatisticsRepositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);

// Serilog yapýlandýrmasý
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Minimum log seviyesi (en düþük: Debug)
    .WriteTo.Console() // Konsola log yaz
    .WriteTo.Debug()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day) // Dosyaya log yaz (günlük dosya)
    .CreateLogger();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddSerilog();
});

builder.Services.AddDbContext<CarBookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarBookDbSettings"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IStatisticsRepository), typeof(StatisticsRepository));
builder.Services.AddScoped(typeof(ICarRepository), typeof(CarRepository));
builder.Services.AddScoped(typeof(IRentACarRepository), typeof(RentACarRepository));

// Service Class'larý Dahil Edilir.
builder.Services.CQRSApplicationService();
builder.Services.AddApplicationService(builder.Configuration);

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
