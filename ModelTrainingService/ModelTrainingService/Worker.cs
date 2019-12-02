using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelTrainingService.DataAccess;
using ModelTrainingService.Helpers;
using ModelTrainingService.Models;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModelTrainingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStorageRepository _storageRepository;
        private readonly Options _options;
        private readonly IPythonHelper _helper;

        public Worker(ILogger<Worker> logger, Options options, IStorageRepository storageRepository,
            IPythonHelper helper)
        {
            _logger = logger;
            _storageRepository = storageRepository;
            _options = options;
            _helper = helper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await _storageRepository.GetAllUserData("all-users.zip");
                ZipFile.ExtractToDirectory("all-users.zip", "raw_data");

                _helper.RunExtractionScript();

                _helper.RunModelTrainigScript();

                PostUserModels();

                _logger.LogInformation("Cleaning up at: {time}", DateTimeOffset.Now);

                CleanDirectory();

                await Task.Delay(TimeSpan.FromHours(_options.WorkerRefreshTime), stoppingToken);
            }
        }

        private void PostUserModels()
        {
            foreach(var file in Directory.EnumerateFiles("trained_models"))
            {
                _storageRepository.PostUserModel(Path.GetFileNameWithoutExtension(file), file);
            }
        }

        private void CleanDirectory()
        {
            Directory
                .GetFiles(".\\", "*.zip", SearchOption.TopDirectoryOnly)
                .ToList()
                .ForEach(File.Delete);
            Directory.Delete(Path.GetFullPath("raw_data"), true);
            Directory.Delete(Path.GetFullPath("processed_data"), true);
            Directory.Delete(Path.GetFullPath("trained_models"), true);
        }
    }
}