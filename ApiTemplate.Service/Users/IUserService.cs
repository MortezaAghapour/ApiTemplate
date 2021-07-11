using System.Threading.Tasks;
using ApiTemplate.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace ApiTemplate.Service.Users
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