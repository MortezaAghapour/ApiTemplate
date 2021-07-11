using System.Threading.Tasks;
using ApiTemplate.Model.Commons;
using ApiTemplate.Model.Jwt;
using ApiTemplate.Model.Users;

namespace ApiTemplate.Factory.Users
{
    public interface IUserFactory
    {
        Task<ResultViewModel<JwtReturnViewModel>> Login(LoginViewModel model);
        Task<ResultViewModel<EmptyViewModel>> Register(RegisterViewModel model);
    }
}