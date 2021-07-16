using System.Threading.Tasks;

namespace ApiTemplate.Service.Rests
{
    public interface IRestService
    {
             Task<T> CallWebService()
    }
}