using System;
using System.Linq;
using System.Threading.Tasks;
using RabitMQTask.Common.Markers.DependencyRegistrar;
using RabitMQTask.Core.Entities.Users;
using RabitMQTask.Mapper.Jwt;
using RabitMQTask.Model.Commons;
using RabitMQTask.Model.Jwt;
using RabitMQTask.Model.Users;
using RabitMQTask.Service.Jwt;
using RabitMQTask.Service.Users;

namespace RabitMQTask.Factory.Users
{
    public class UserFactory : IUserFactory, IScopedDependency
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        #endregion
        #region Constructors
        public UserFactory(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        #endregion
        #region Methods
        public async Task<ResultViewModel<JwtReturnViewModel>> Login(LoginViewModel model)
        {
            var result = new ResultViewModel<JwtReturnViewModel>
            {
                IsSuccess = false
            };
            var user = await _userService.GetUserByUserName(model.UserName);
            if (user is null)
            {
                result.Errors.Add("کاربری با مشخصات وارد شده یافت نشد");
                return result;
            }

            var checkPassword = await _userService.CheckPassword(user, model.Password);
            if (checkPassword.IsLockedOut)
            {
                result.Errors.Add("کاربر مورد نظر مسدود می باشد");
                return result;
            }

            if (checkPassword.IsNotAllowed)
            {
                result.Errors.Add("کاربر مورد اجازه ورورد به سیستم را ندارد");
                return result;
            }

            await _userService.UpdateLockoutToZero(user);

            result.IsSuccess = true;
            var jwt = await _jwtService.Generate(user);
            result.Data = JwtMapper.ToJwtReturnViewModel(jwt);
            return result;
        }

        public async Task<ResultViewModel<EmptyViewModel>> Register(RegisterViewModel model)
        {
            var result = new ResultViewModel<EmptyViewModel>
            {
                IsSuccess = false
            };
            var checkUserName = await _userService.GetUserByUserName(model.UserName);
            if (!(checkUserName is null))
            {
                result.Errors.Add("کاربری با نام کاربری وارد شده در سیستم موجود می باشد");
                return result;
            }

            var appUser = new AppUser
            {
                Name = model.Name,
                LastName = model.LastName,
                UserName = model.UserName
            };
            var register = await _userService.AddUser(appUser, model.Password);
            if (!register.Succeeded)
            {
                result.Errors.AddRange(register.Errors?.Select(c => c.Description));
                return result;
            }

            result.IsSuccess = true;
            result.Message = "کاربر مورد نظر با موفقیت ثبت شد";
            return result;
        }

        #endregion

    }
}