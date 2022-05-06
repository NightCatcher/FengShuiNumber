// See https://aka.ms/new-console-template for more information
using FengShuiNumber;
using FengShuiNumber.Data;
using FengShuiNumber.FengshuiFilter;
using FengShuiNumber.FengshuiFilter.Interfaces;
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
services.AddScoped<IFengShuiFilter, FengShuiRateFilter>();
services.AddScoped<IFengShuiFilter, HeaderNumberFilter>();
services.AddScoped<IFengShuiFilter, NiceLastPairFilter>();
services.AddScoped<IFengShuiFilter, TabooPairFilter>();
services.AddScoped<IFengShuiFilter, FengshuiLengthFilter>();
services.AddScoped<IFengShuiFilterComposer, FengShuiFilterComposer>();
services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
services.AddScoped<IFengShuiNumberService, FengShuiNumberService>();

var serviceProvider = services.BuildServiceProvider();
// seed data
 var dataSeeder = serviceProvider.GetRequiredService<IDataSeeder>();
await dataSeeder.SeedAsync();

// get excutor and do the job
var numberService = serviceProvider.GetRequiredService<IFengShuiNumberService>();
CheckFengShuiNumber:
var fengShuiNumbers = await numberService.GetFengShuiNumberAsync();
if(fengShuiNumbers == null || !fengShuiNumbers.Any())
    Console.WriteLine("There no feng shui number");
else
    Console.WriteLine(String.Join('\n', fengShuiNumbers));

Console.WriteLine("Try again? [y/n]");
var answer = Console.ReadLine();
if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
    goto CheckFengShuiNumber;




