using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Core.Entities.Weathers;

namespace ApiTemplate.Service.Weathers
{
    public interface IWeatherService
    {
        Task AddWeather(Weather weather, CancellationToken cancellationToken=default);
    }
}