using RabitMQTask.Common.Markers.DependencyRegistrar;
using RabitMQTask.Core.Entities.Weathers;
using RabitMQTask.Data.ApplicationDbContexts;
using RabitMQTask.Data.Repositories.Base;

namespace RabitMQTask.Data.Repositories.Weathers
{
    public class WeatherRepository : Repository<Weather>,IWeatherRepository ,IScopedDependency
    {
        public WeatherRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}