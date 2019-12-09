using AccelerometerStorage.Business;
using RabbitMQ.Client;
using System;
using System.Text;

namespace AccelerometerStorage.Infrastructure.Helpers
{
    public class QueueHelper : IDisposable, IQueueHelper
    {
        private readonly QueueSettings _settings;
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public QueueHelper(QueueSettings settings)
        {
            _settings = settings;

            var factory = new ConnectionFactory() { HostName = _settings.QueueEndpoint };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void EnqueueMessage(string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                     routingKey: _settings.QueueName,
                     basicProperties: null,
                     body: encodedMessage);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}

