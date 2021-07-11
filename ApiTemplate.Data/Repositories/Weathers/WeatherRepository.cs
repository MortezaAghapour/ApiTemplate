using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Entities.Weathers;
using ApiTemplate.Data.ApplicationDbContexts;
using ApiTemplate.Data.Repositories.Base;

namespace ApiTemplate.Data.Repositories.Weathers
{
    public class WeatherRepository : Repository<Weather>,IWeatherRepository ,IScopedDependency
    {
        public WeatherRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}