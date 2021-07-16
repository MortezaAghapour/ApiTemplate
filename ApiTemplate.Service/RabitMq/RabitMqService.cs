using System;
using System.Text;
using System.Threading.Tasks;
using ApiTemplate.Common.Enums.RabitMq;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Configurations.RabitMq;
using ApiTemplate.Core.DataTransforObjects.RabitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ApiTemplate.Service.RabitMq
{
    public class RabitMqService : IRabitMqService, IScopedDependency
    {
        #region Fields
        private readonly RabitMqConfiguration _rabitMqConfiguration;
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;
        private const string QueueName = "Standard.Queue";
        private const string ExchangeName = "Fanout.Exchange";
        #endregion

        #region Constructors
        public RabitMqService(RabitMqConfiguration rabitMqConfiguration)
        {
            _rabitMqConfiguration = rabitMqConfiguration;
        }
        #endregion
        #region Methods
        public void CreateQueue(ExchangeTypes type)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabitMqConfiguration.Host,
                UserName = _rabitMqConfiguration.UserName,
                Password = _rabitMqConfiguration.Password,
                DispatchConsumersAsync = true,
                Port = _rabitMqConfiguration.Port
            };

            _connection = _connectionFactory.CreateConnection();

            _model = _connection.CreateModel();

            switch (type)
            {
                case ExchangeTypes.Direct:
                    _model.QueueDeclare(QueueName, true, false, false, null);
                    break;
                case ExchangeTypes.Fanout:
                    _model.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, true);
                    break;
                default:
                   break;
            }

        }

        public void SendMessage<T>(T model, ExchangeTypes type) where T : class
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var byteMessage = Encoding.UTF8.GetBytes(jsonModel);
            //exchange ==null => Direct type
            /*
         * Exchange type
         *  1-Direct
         * 2-Fan Out   (pub/sub)
         * 3-Topic
         * 4-Header
         */
            switch (type)
            {
               
                case ExchangeTypes.Direct:
                {
                    var basicProperties = _model.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    _model.BasicPublish(string.Empty, QueueName, false, basicProperties, byteMessage);
                    break;
                }
                case ExchangeTypes.Fanout:
                    _model.BasicPublish(ExchangeName, string.Empty, false, null, byteMessage);
                    break;
                default:
                    break;
            }

            if (_model.IsOpen)
            {
                _model.Close();
            }

            if (_connection.IsOpen)
            {
                _connection.Close();
            }
        }

        public async Task<T> ReadMessage<T>(ExchangeTypes type) where T : class
        {
            T result = null;
            var queueName = string.Empty;
            switch (type)
            {
                case ExchangeTypes.Direct:
                    _model.BasicQos(0, 1, false);
                    break;
                case ExchangeTypes.Fanout:
                    queueName = _model.QueueDeclare().QueueName;
                    _model.QueueBind(queueName, ExchangeName, string.Empty);
                    break;
                default:
                   break;
            }

            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var stringBody = Encoding.UTF8.GetString(body);
                result = JsonConvert.DeserializeObject<T>(stringBody);
                _model.BasicAck(ea.DeliveryTag, false);
            };
            switch (type)
            {
                case ExchangeTypes.Direct:
                    _model.BasicConsume(QueueName, false, consumer);
                    break;
                case ExchangeTypes.Fanout:
                    _model.BasicConsume(queueName, true, consumer);
                    break;
                default:
                    break;
            }
            if (_model.IsOpen)
            {
                _model.Close();
            }
            if (_connection.IsOpen)
            {
                _connection.Close();
            }

            return await Task.FromResult(result);
        }




        #endregion

    }
}