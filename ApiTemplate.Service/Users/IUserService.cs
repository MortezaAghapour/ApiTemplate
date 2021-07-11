using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Service.Users
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUserName(string userName);
        Task<SignInResult> CheckPassword(AppUser user, string password);
        Task<AppUser> GetUserById(long id);
        Task<IdentityResult> AddUser(AppUser appUser,string password);
        Task UpdateLockoutToZero(AppUser user);
    }
}