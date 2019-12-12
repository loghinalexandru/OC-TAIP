using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelTrainingService.DataAccess;
using ModelTrainingService.Helpers;
using ModelTrainingService.Models;
using ModelTrainingService.Models.Interfaces;

namespace ModelTrainingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    var options = configuration.GetSection("Options").Get<Options>();
                    services.AddSingleton(options);
                    services.AddSingleton<IScriptRunner, ScriptRunner>();

                    services.AddTransient<IStorageRepository, StorageRepository>();
                    services.AddHostedService<Worker>();
                });
    }
}