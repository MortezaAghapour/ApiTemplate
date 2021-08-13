using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Entities.Weathers;
using ApiTemplate.Data.Repositories.Weathers;

namespace ApiTemplate.Service.Weathers
{
    public class WeatherService : IWeatherService, IScopedDependency
    {
        #region Fields

        private readonly IWeatherRepository _weatherRepository;

        #endregion

        #region Constructors
        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        #endregion

        #region Methods
        public async Task AddWeather(Weather weather, CancellationToken cancellationToken = default)
        {
            if (weather is null)
            {
                throw new ArgumentNullException($"پارامتر {nameof(weather)} در کلاس {GetType().Name}  و متد {MethodBase.GetCurrentMethod().Name} خالی می باشد");
            }

            await _weatherRepository.Insert(weather, cancellationToken);
        }

        #endregion


    }
}