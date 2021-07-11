using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabitMQTask.Common.Exceptions;
using RabitMQTask.Common.Markers.DependencyRegistrar;
using RabitMQTask.Core.Entities.Users;
using RabitMQTask.Data.UnitOfWorks;

namespace RabitMQTask.Service.Users
{
    public class UserService : IUserService   ,IScopedDependency
    {
        #region Fields

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
       

        #endregion
        #region Constructors
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
         
        }
        #endregion
        #region Methods
        public async Task<AppUser> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new NullArgumentException($"فیلد {nameof(userName)} در کلاس  {GetType().Name} در متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");
            }

            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<SignInResult> CheckPassword(AppUser user, string password)
        {
            if (user is null)
            {
                throw new NullArgumentException($"فیلد {nameof(user)} در کلاس  {GetType().Name} در متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new NullArgumentException($"فیلد {nameof(password)} در کلاس  {GetType().Name} در متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");
            }

            return await _signInManager.CheckPasswordSignInAsync(user, password, true);

        }

        public async Task<AppUser> GetUserById(long id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<IdentityResult> AddUser(AppUser appUser, string password)
        {
            if (appUser is null)
            {
                throw new NullArgumentException($"فیلد {nameof(appUser)} در کلاس  {GetType().Name} در متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");

            }

            if (string.IsNullOrEmpty(password))
            {
                throw new NullArgumentException($"فیلد {nameof(password)} در کلاس  {GetType().Name} در متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");

            }

            var result = await _userManager.CreateAsync(appUser, password);
            return result;
        }

        public async Task UpdateLockoutToZero(AppUser user)
        {
            await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.UtcNow));
        }

        #endregion

    }
}