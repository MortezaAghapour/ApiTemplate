using ApiTemplate.Common.Markers.DependencyRegistrar;

namespace ApiTemplate.Service.RabbitMq
{
    public class ProducerService : IProducerService,IScopedDependency
    {
        #region Fields

        private readonly IRabbitMqService _rabbitMqService;

        #endregion

        #region Constructors
        public ProducerService(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        #endregion
        #region Methods
        public void SendMessage()
        {

           
        }
        #endregion

    }
}