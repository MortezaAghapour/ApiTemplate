using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Data.Repositories.Weathers;

namespace ApiTemplate.Data.UnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        IWeatherRepository WeatherRepository { get; }
      
        Task SaveChange(CancellationToken cancellationToken);
    }
}