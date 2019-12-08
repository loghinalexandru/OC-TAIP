using ModelPredictingService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelPredictingService.Helpers
{
    public class QueueHelper : IQueueHelper
    {
        private readonly string _queueEndpoint;
        private readonly string _queueName;

        public QueueHelper(Options options)
        {
            _queueEndpoint = options.QueueEndpoint;
            _queueName = options.QueueName;
        }

        public void RegisterEventHandler(EventHandler<BasicDeliverEventArgs> handler)
        {
            var factory = new ConnectionFactory() { HostName = _queueEndpoint };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += handler;

            channel.BasicConsume(_queueName, true, consumer);
        }
    }
}
