using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Data.ApplicationDbContexts;
using ApiTemplate.Data.Repositories.Weathers;

namespace ApiTemplate.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork  ,IScopedDependency
    {
        #region Fields

        private readonly AppDbContext _appDbContext;
        private readonly IWeatherRepository _weatherRepository;
      
        #endregion
        #region Constructors
        public UnitOfWork(AppDbContext appDbContext, IWeatherRepository weatherRepository)
        {
            _appDbContext = appDbContext;
            _weatherRepository = weatherRepository;
          
        }

        #endregion
        #region Methods

        public IWeatherRepository WeatherRepository =>
            _weatherRepository ?? new WeatherRepository(_appDbContext);    
     
        public async Task SaveChange(CancellationToken cancellationToken)
        {
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appDbContext.Dispose();
            }
        }
        #endregion


    }
}