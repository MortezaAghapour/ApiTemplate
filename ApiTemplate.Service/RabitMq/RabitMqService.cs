using System.Text;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Configurations.RabitMq;
using ApiTemplate.Core.DataTransforObjects.RabitMq;
using RabbitMQ.Client;

namespace ApiTemplate.Service.RabitMq
{
    public class RabitMqService : IRabitMqService, IScopedDependency
    {
        #region Fields
        private readonly RabitMqConfiguration _rabitMqConfiguration;
        #endregion

        #region Constructors
        public RabitMqService(RabitMqConfiguration rabitMqConfiguration)
        {
            _rabitMqConfiguration = rabitMqConfiguration;
        }
        #endregion
        #region Methods
        public IConnection Channel()
        {
            var connectionFactory=new ConnectionFactory
            {
                HostName = _rabitMqConfiguration.Host,
                UserName = _rabitMqConfiguration.UserName,
                Password = _rabitMqConfiguration.Password,
                DispatchConsumersAsync = true,
                Port =_rabitMqConfiguration.Port
            };
            var channel = connectionFactory.CreateConnection();
            return channel;
        }

        public void Publish(PublishMessageModel publishMessageModel)
        {
            var channel = Channel();
            var model = channel.CreateModel();
            model.QueueDeclare(publishMessageModel.QueueName, false, false, false, null);
            var byteMessage = Encoding.UTF8.GetBytes(publishMessageModel.Message);
            //exchange ==null => Direct type
            /*
             * Exchange type
             *  1-Direct
             * 2-Fan Out
             * 3-Topic
             * 4-Header
             */
            model.BasicPublish(string.Empty,publishMessageModel.RouteKey,false,null,byteMessage);
        }

        #endregion

    }
}