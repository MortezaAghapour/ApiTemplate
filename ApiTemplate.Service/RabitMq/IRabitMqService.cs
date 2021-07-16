using System.Threading.Tasks;
using ApiTemplate.Common.Enums.RabitMq;
using ApiTemplate.Core.DataTransforObjects.RabitMq;
using RabbitMQ.Client;

namespace ApiTemplate.Service.RabitMq
{
    public interface IRabitMqService
    {
        void CreateQueue(ExchangeTypes type);
        void SendMessage<T>(T model, ExchangeTypes type) where T:class;
        Task<T> ReadMessage<T>(ExchangeTypes type) where T:class;
    }
}