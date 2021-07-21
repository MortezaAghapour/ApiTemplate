using System.Threading.Tasks;
using ApiTemplate.Core.DataTransforObjects.Rest;

namespace ApiTemplate.Service.Rests
{
    public interface IRestService
    {
        Task<ResponseModel<TResult>> Call<TResult, TParam>(CallParameterModel<TParam> parameter) where TResult : class;
    }
}