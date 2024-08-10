using Common;
using Common.Services;
using EasyNetQ;

namespace Persistence.Services
{
    public class RabbitMqPublishService : IPublishService
    {
        private readonly IBus _bus;
        public RabbitMqPublishService(IBus bus)
        {
            _bus = bus;
        }

        public void PublishMessage(RabbitMqMessage message)
        {
            _bus.PubSub.Publish(message);
        }
    }
}
