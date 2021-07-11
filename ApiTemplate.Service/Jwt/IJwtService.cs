using System.Threading.Tasks;
using RabitMQTask.Core.DataTransforObjects.Jwt;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Service.Jwt
{
    public interface IJwtService
    {
        Task<JwtReturnModel> Generate(AppUser user);
    }
}