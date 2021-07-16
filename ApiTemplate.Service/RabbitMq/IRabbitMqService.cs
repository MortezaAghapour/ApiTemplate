using System;
using System.Threading.Tasks;
using ApiTemplate.Common.Enums.RabbitMq;

namespace ApiTemplate.Service.RabbitMq
{
    public interface IRabbitMqService   :IDisposable
    {
        void CreateQueue(ExchangeTypes type);
        void SendMessage<T>(T model, ExchangeTypes type) where T:class;
        Task<T> ReadMessage<T>(ExchangeTypes type) where T:class;
    }
}