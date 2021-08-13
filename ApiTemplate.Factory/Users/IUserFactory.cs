using System.Threading.Tasks;
using ApiTemplate.Model.Commons;
using ApiTemplate.Model.Jwt;
using ApiTemplate.Model.Users;

namespace ApiTemplate.Factory.Users
{
    public interface IUserFactory
    {
        Task<ReturnModel<JwtReturnModel>> Login(LoginModel model);
        Task<ReturnModel<EmptyModel>> Register(RegisterModel model);
    }
}