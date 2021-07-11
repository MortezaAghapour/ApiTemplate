using System;
using System.Threading;
using System.Threading.Tasks;
using RabitMQTask.Data.Repositories.Base;

using RabitMQTask.Data.Repositories.Weathers;

namespace RabitMQTask.Data.UnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        IWeatherRepository WeatherRepository { get; }
      
        Task SaveChange(CancellationToken cancellationToken);
    }
}