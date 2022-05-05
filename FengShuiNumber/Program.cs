// See https://aka.ms/new-console-template for more information
using FengShuiNumber;
using FengShuiNumber.Data;
using FengShuiNumber.Repositories;
using FengShuiNumber.Repositories.Interfaces;
using FengShuiNumber.Services;
using FengShuiNumber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("App starting...!!!");

var services = new ServiceCollection();
// set up appsettings file
var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var configuration = builder.Build();
services.Configure<FengShuiNumberConfiguration>(configuration.GetSection("FengShuiNumberConfiguration"));

// register services
services.AddDbContext<ApplicationDbContext>(opt =>
{
    var connectionString = configuration["ConnectionString"];
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
services.AddScoped<IDataSeeder, DataSeeder>();
services.AddScoped<IFengShuiFilter, FengShuiFilter>();
services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
services.AddScoped<IFengShuiNumberService, FengShuiNumberService>();

var serviceProvider = services.BuildServiceProvider();
// seed data
 var dataSeeder = serviceProvider.GetRequiredService<IDataSeeder>();
await dataSeeder.SeedAsync();

// get excutor and do the job
var numberService = serviceProvider.GetRequiredService<IFengShuiNumberService>();
var fengShuiNumbers = await numberService.GetFengShuiNumberAsync();
if(fengShuiNumbers == null || !fengShuiNumbers.Any())
    Console.WriteLine("There no feng shui number");
else
    Console.WriteLine(String.Join('\n', fengShuiNumbers));

Console.ReadKey();


