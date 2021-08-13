using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Model.Rest;

namespace ApiTemplate.Service.Rests
{
    public interface IRestService
    {
        Task<ResponseModel<TResult>> CallPost<TResult, TParam>(CallParameterModel<TParam> parameter,CancellationToken cancellationToken=default) where TResult : class;
        Task<ResponseModel<TResult>> CallSend<TResult>(SendParameterModel parameter, CancellationToken cancellationToken = default) where TResult : class;
    }
}