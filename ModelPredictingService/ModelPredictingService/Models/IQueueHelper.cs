using System;
using RabbitMQ.Client.Events;

namespace ModelPredictingService.Models
{
    public interface IQueueHelper
    {
        void RegisterEventHandler(EventHandler<BasicDeliverEventArgs> handler);
    }
}
