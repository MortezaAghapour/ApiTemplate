using System.Threading.Tasks;
using ApiTemplate.Core.DataTransforObjects.RabitMq;
using RabbitMQ.Client;

namespace ApiTemplate.Service.RabitMq
{
    public interface IRabitMqService
    {
        IConnection Channel();
        void Publish(PublishMessageModel publishMessageModel);
    }
}