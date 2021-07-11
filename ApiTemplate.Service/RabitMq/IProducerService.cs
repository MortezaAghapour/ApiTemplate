using ApiTemplate.Core.DataTransforObjects.RabitMq;

namespace ApiTemplate.Service.RabitMq
{
    public interface IProducerService
    {
        void SendMessage(PublishMessageModel model);
    }
}