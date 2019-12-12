using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelPredictingService.Helpers;
using ModelPredictingService.Models;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModelPredictingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueHelper _queueHelper;
        private readonly IStorageRepository _storageRepository;
        private readonly Options _options;

        public Worker(ILogger<Worker> logger, IQueueHelper queueHelper, IStorageRepository storageRepository,
            Options options)
        {
            _logger = logger;
            _queueHelper = queueHelper;
            _storageRepository = storageRepository;
            _options = options;

            InitQueue();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            SetWorkingDirectory();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private void InitQueue()
        {
            _queueHelper.RegisterEventHandler(Consumer);
        }

        private async void Consumer(object sender, BasicDeliverEventArgs args)
        {
            var username = Encoding.UTF8.GetString(args.Body);

            try
            {
                await _storageRepository.GetUserModel(username);
                ZipFile.ExtractToDirectory(username + ".zip", username + "_models");

                var modelPath = Directory.GetFiles(username + "_models").FirstOrDefault();

                if (modelPath == null)
                {
                    return;
                }

                var scriptRunner = new ScriptRunner(_options.PythonFullPath);
                scriptRunner.Execute(new PredictionScript(_options.ModelPredictionScriptPath));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            finally
            {
                CleanDirectory(username);
            }
        }

        private void SetWorkingDirectory()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void CleanDirectory(string username)
        {
            Directory
                .GetFiles(".\\", "*.zip", SearchOption.TopDirectoryOnly)
                .ToList()
                .ForEach(File.Delete);

            if (Directory.Exists(Path.GetFullPath(username + "_models")))
            {
                Directory.Delete(Path.GetFullPath(username + "_models"), true);
            }
        }
    }
}