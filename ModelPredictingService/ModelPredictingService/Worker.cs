using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelPredictingService.Helpers;
using ModelTrainingService.DataAccess;
using RabbitMQ.Client.Events;

namespace ModelPredictingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueHelper _queueHelper;
        private readonly IStorageRepository _storageRepository;

        public Worker(ILogger<Worker> logger, IQueueHelper queueHelper, IStorageRepository storageRepository)
        {
            _logger = logger;
            _queueHelper = queueHelper;
            _storageRepository = storageRepository;

            InitQueue();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
        }

        private void InitQueue()
        {
            _queueHelper.RegisterEventHandler(Consumer);
        }

        private void Consumer(object sender, BasicDeliverEventArgs args)
        {
            var username = Encoding.UTF8.GetString(args.Body);


        }
    }
}
