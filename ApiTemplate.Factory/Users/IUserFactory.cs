using System.Threading.Tasks;
using RabitMQTask.Model.Commons;
using RabitMQTask.Model.Jwt;
using RabitMQTask.Model.Users;


namespace RabitMQTask.Factory.Users
{
    public interface IUserFactory
    {
        Task<ResultViewModel<JwtReturnViewModel>> Login(LoginViewModel model);
        Task<ResultViewModel<EmptyViewModel>> Register(RegisterViewModel model);
    }
}