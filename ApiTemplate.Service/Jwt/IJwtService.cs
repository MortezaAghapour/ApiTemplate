using System.Threading.Tasks;

using ApiTemplate.Core.Entities.Users;
using ApiTemplate.Model.Jwt;

namespace ApiTemplate.Service.Jwt
{
    public interface IJwtService
    {
        Task<JwtReturnModel> Generate(AppUser user);
    }
}