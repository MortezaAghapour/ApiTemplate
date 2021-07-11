using RabitMQTask.Core.Entities.Weathers;
using RabitMQTask.Data.Repositories.Base;

namespace RabitMQTask.Data.Repositories.Weathers
{
    public interface IWeatherRepository  :IRepository<Weather>
    {
        
    }
}