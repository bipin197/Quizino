using Common;
using Common.Services;
using MassTransit;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RabbitMqPublishService : IPublishService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public RabbitMqPublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishMessage(Message message)
        {
           await _publishEndpoint.Publish(message);
        }
    }
}
