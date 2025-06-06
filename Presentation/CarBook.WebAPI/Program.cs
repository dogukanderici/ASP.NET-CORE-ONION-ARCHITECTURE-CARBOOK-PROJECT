using CarBook.Application;
using CarBook.Application.Interfaces;
using CarBook.Application.Interfaces.CarInterfaces;
using CarBook.Application.Interfaces.RentACarInterfaces;
using CarBook.Application.Interfaces.StatisticsInterfaces;
using CarBook.Application.Interfaces.TokenInterfaces;
using CarBook.Application.Services;
using CarBook.Configurations;
using CarBook.Persistance.Context;
using CarBook.Persistance.Repositories;
using CarBook.Persistance.Repositories.CarRepositories;
using CarBook.Persistance.Repositories.RentACarRepositories;
using CarBook.Persistance.Repositories.StatisticsRepositories;
using CarBook.Persistance.Services.TokenServices;
using CarBook.WebAPI.Authorization.Requirements;
using CarBook.WebAPI.Utilities.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidations();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT BEARER CONFUGURATIONS
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration.GetSection("ApiUrlOptions").Get<ApiUrlOptions>().IdentityServerUrl; // IdentityServer Url
        options.Audience = "CarBookFullPermission"; // Config dosyas�ndaki API Resource
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = "role",
            NameClaimType = "name",
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// API Authorization Configurations
builder.Services.AddAuthorizationServices();

builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);

// Serilog yap�land�rmas�
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Minimum log seviyesi (en d���k: Debug)
    .WriteTo.Console() // Konsola log yaz
    .WriteTo.Debug()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day) // Dosyaya log yaz (g�nl�k dosya)
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


// Service Class'lar� Dahil Edilir.
builder.Services.CQRSApplicationService();
builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.Configure<ApiUrlOptions>(builder.Configuration.GetSection("ApiUrlOptions"));
builder.Services.Configure<IdentityServerOptions>(builder.Configuration.GetSection("IdentityServerOptions"));
builder.Services.AddScoped<ITokenService, TokenService>();

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
