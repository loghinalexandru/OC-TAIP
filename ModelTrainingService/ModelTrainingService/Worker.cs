using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelTrainingService.DataAccess;
using ModelTrainingService.Helpers;
using ModelTrainingService.Models;
using System;
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

                var users = await _storageRepository.GetUsers();
                foreach (var user in users)
                {
                    await TrainModelForUser(user.Username);
                }

                await Task.Delay(TimeSpan.FromHours(_options.WorkerRefreshTime), stoppingToken);
            }
        }

        private async Task TrainModelForUser(string username)
        {
            await _storageRepository.GetAllUserData("all-users.zip");
            await _storageRepository.GetUserData(username, username + ".zip");

            _helper.RunScript();
        }
    }
}