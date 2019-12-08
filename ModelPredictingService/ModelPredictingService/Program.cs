
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelPredictingService.Helpers;
using ModelPredictingService.Models;
using ModelTrainingService.DataAccess;

namespace ModelPredictingService
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

                    services.AddTransient<IQueueHelper, QueueHelper>();
                    services.AddTransient<IStorageRepository, StorageRepository>();
                    services.AddHostedService<Worker>();
                });
    }
}
