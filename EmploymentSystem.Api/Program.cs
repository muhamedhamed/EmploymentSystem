using EmploymentSystem.Application.Services;
using EmploymentSystem.Domain.Interfaces.Repositories;
using EmploymentSystem.Infrastructure;
using EmploymentSystem.Infrastructure.Repositories;
using EmploymentSystem.Application.Interfaces;
using App.Application;
using Microsoft.EntityFrameworkCore;
using EmploymentSystem.Application.Mapper;
using Serilog;
using System.Text;
using Microsoft.IdentityModel.Tokens;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/employment_system_info.txt", rollingInterval: RollingInterval.Day) // create log file daily
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", false, true)
  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
  .AddEnvironmentVariables();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// I prefer the following way to access database but I use the onConfigure way becasue I get some errors with this way.
#region DataBase Context
var connectionString = builder.Configuration.GetConnectionString("EmploymentDB");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<AppDbContext>();
#endregion

builder.Services.AddCors();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<IApplicationVacancyRepository, ApplicationVacancyRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IApplicationVacancyService, ApplicationVacancyService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                             Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //generate specification and info about our project
    app.UseSwaggerUI(); // use the specification generated and generate the UI
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
