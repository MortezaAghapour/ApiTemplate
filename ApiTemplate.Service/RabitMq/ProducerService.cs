using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.DataTransforObjects.RabitMq;

namespace ApiTemplate.Service.RabitMq
{
    public class ProducerService : IProducerService,IScopedDependency
    {
        #region Fields

        private readonly IRabitMqService _rabitMqService;



        #endregion

        #region Constructors
        public ProducerService(IRabitMqService rabitMqService)
        {
            _rabitMqService = rabitMqService;
        }
        #endregion
        #region Methods
        public void SendMessage(PublishMessageModel model)
        {

            _rabitMqService.Publish(model);
        }
        #endregion

    }
}