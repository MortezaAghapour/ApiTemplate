using System.Threading.Tasks;
using ApiTemplate.Core.DataTransforObjects.Jwt;
using ApiTemplate.Core.Entities.Users;

namespace ApiTemplate.Service.Jwt
{
    public interface IJwtService
    {
        Task<JwtReturnModel> Generate(AppUser user);
    }
}