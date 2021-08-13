using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Model.Weathers;

namespace ApiTemplate.Factory.Weathers
{
    public interface IWeatherFactory
    {
        Task GetCurrentWeatherInfoByCityName(CurrentWeatherByCityRequestModel model,CancellationToken cancellationToken=default);
    }
}