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
using ModelTrainingService.Models.Interfaces;

namespace ModelTrainingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStorageRepository _storageRepository;
        private readonly Options _options;
        private readonly IScriptRunner _scriptRunner;

        public Worker(ILogger<Worker> logger,
            Options options,
            IStorageRepository storageRepository,
            IScriptRunner scriptRunner)
        {
            _logger = logger;
            _storageRepository = storageRepository;
            _options = options;
            _scriptRunner = scriptRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(AppDomain.CurrentDomain.BaseDirectory);

            SetWorkingDirectory();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    await GetStorageUserData();

                    _scriptRunner.Execute(new FeatureExtractionScript(_options.DataPreprocessingScriptPath));

                    _scriptRunner.Execute(new ModelGenerationScript(_options.ModelGenerationScriptPath));

                    PostUserModels();

                    _logger.LogInformation("Cleaning up at: {time}", DateTimeOffset.Now);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                }
                finally
                {
                    CleanDirectory();
                }

                await Task.Delay(TimeSpan.FromHours(_options.WorkerRefreshTime), stoppingToken);
            }
        }

        private void PostUserModels()
        {
            foreach (var file in Directory.EnumerateFiles("trained_models"))
            {
                _storageRepository.PostUserModel(Path.GetFileNameWithoutExtension(file), file);
            }
        }

        private async Task GetStorageUserData()
        {
            await _storageRepository.GetAllUserData("all-users.zip");
            ZipFile.ExtractToDirectory("all-users.zip", "raw_data");
        }

        private void SetWorkingDirectory()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void CleanDirectory()
        {
            Directory
                .GetFiles(".\\", "*.zip", SearchOption.TopDirectoryOnly)
                .ToList()
                .ForEach(File.Delete);

            SafeDirectoryDelete("raw_data");
            SafeDirectoryDelete("processed_data");
            SafeDirectoryDelete("trained_models");
        }

        private void SafeDirectoryDelete(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(Path.GetFullPath(directoryPath), true);
            }
        }
    }
}