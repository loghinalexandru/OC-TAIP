using RabbitMQ.Client.Events;
using System;

namespace ModelPredictingService.Helpers
{
    public interface IQueueHelper
    {
        void RegisterEventHandler(EventHandler<BasicDeliverEventArgs> handler);
    }
}
