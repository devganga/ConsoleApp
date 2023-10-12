using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using consoleApp;
using consoleApp.AppSettings;
using ConsoleApp.Services;

var isValid = Parser.Default.ParseArguments<CustomCommandLineOptions>(args);

var builder = new ConfigurationBuilder();

if (isValid != null)
{
    Console.WriteLine(isValid.Value.Environment);
}

Configuration.BuildConfig(builder, isValid!.Value.Environment!);


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Application Starting");

var host = Host.CreateDefaultBuilder()
.ConfigureServices((context, services) =>
{
    //configure Browser
    Configuration.BrowserConfig(services);

    //App Settings configuration
    Configuration.AppSettingsConfig(context.Configuration, services);    

    //Register Services 
    Configuration.DepandancyInjectionConfig(services);
})
.UseSerilog()
.Build();


//Get the Service to Execute
var svc = ActivatorUtilities.CreateInstance<RunService>(host.Services);
await svc.Run();
Log.Logger.Information("Application Ended");

Console.ReadLine();
//terminate console application
Environment.Exit(0);