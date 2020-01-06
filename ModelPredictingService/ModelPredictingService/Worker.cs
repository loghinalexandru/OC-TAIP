using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelPredictingService.Helpers;
using ModelPredictingService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.IO;
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
        private readonly IScriptRunner _scriptRunner;
        private readonly IEmailHelper _emailHelper;

        public Worker(
            ILogger<Worker> logger,
            IQueueHelper queueHelper,
            IStorageRepository storageRepository,
            IScriptRunner scriptRunner,
            IEmailHelper emailHelper,
            Options options)
        {
            _logger = logger;
            _queueHelper = queueHelper;
            _storageRepository = storageRepository;
            _scriptRunner = scriptRunner;
            _emailHelper = emailHelper;
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
            var processGuid = Guid.NewGuid();
            var userModel = JsonConvert.DeserializeObject<UserModel>(Encoding.UTF8.GetString(args.Body));

            try
            {
                await _storageRepository.GetLatestUserData(userModel.Username, processGuid.ToString());
                await _storageRepository.GetLatestUserModel(userModel.Username, processGuid.ToString());

                _scriptRunner.Execute(new FeatureExtractionScript(_options.DataPreprocessingScriptPath,
                    userModel.Username,
                    processGuid.ToString()));
                _scriptRunner.Execute(new PredictionScript(_options.ModelPredictionScriptPath, userModel.Username,
                    processGuid.ToString()));

                var matchCoefficient = GetMatchCoefficent($"predictions_{processGuid}_{userModel.Username}");

                if(matchCoefficient < 51)
                {
                    _emailHelper.SendEmail(userModel.Email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            finally
            {
                CleanDirectory(userModel.Username, processGuid.ToString());
            }
        }

        private void SetWorkingDirectory()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void CleanDirectory(string username, string processGuid)
        {
            Directory
                .GetDirectories(".\\", $"*{processGuid}_{username}", SearchOption.TopDirectoryOnly)
                .ToList()
                .ForEach(path => Directory.Delete(path, true));
        }

        private decimal GetMatchCoefficent(string path)
        {
            var predictionFile = Directory.GetFiles(path).FirstOrDefault();
            decimal factor = 0;
            long secondsCount = 0;

            if(predictionFile == null)
            {
                throw new ArgumentException("No prediction file has been generated!");
            }

            var lines = File.ReadLines(predictionFile);

            foreach(var line in lines)
            {
                factor += decimal.Parse(line, System.Globalization.NumberStyles.Float);
                secondsCount += 1;
            }

            return factor / secondsCount;
        }
    }
}