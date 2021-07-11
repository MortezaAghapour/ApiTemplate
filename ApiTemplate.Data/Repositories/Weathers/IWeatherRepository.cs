using ApiTemplate.Core.Entities.Weathers;
using ApiTemplate.Data.Repositories.Base;

namespace ApiTemplate.Data.Repositories.Weathers
{
    public interface IWeatherRepository  :IRepository<Weather>
    {
        
    }
}