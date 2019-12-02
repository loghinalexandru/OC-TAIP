using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelTrainingService.DataAccess;
using ModelTrainingService.Helpers;
using ModelTrainingService.Models;

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
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;

                    var options = configuration.GetSection("Options").Get<Options>();
                    services.AddSingleton(options);

                    services.AddTransient<IStorageRepository, StorageRepository>();
                    services.AddTransient<IPythonHelper, PythonHelper>();
                    services.AddHostedService<Worker>();
                });
    }
}