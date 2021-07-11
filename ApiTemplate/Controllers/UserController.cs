using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RabitMQTask.Extensions.ModelStates;
using RabitMQTask.Factory.Users;
using RabitMQTask.Model.Commons;
using RabitMQTask.Model.Users;

namespace RabitMQTask.Controllers
{
    public class UserController : BaseController
    {
        #region Fields

        private readonly IUserFactory _userFactory;

        #endregion

        #region Constructors
        public UserController(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }
        #endregion

        #region Actions
        [HttpGet("[action]")]
        public IActionResult Test()
        {
            return Ok("Ok");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<EmptyViewModel>
                {
                    IsSuccess = false,
                    Errors = ModelState.GetModelStateErrors()
                });
            var login =await _userFactory.Login(model);
            if (login.IsSuccess)
            {
                return Ok(login);
            }
            else
            {
                return BadRequest(login);
            }
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<EmptyViewModel>
                {
                    IsSuccess = false,
                    Errors = ModelState.GetModelStateErrors()
                });
            var register =await _userFactory.Register(model);
            if (register.IsSuccess)
            {
                return Ok(register);
            }
            else
            {
                return BadRequest(register);
            }
        }
        #endregion
    }
}
