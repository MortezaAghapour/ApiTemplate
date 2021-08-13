﻿using System.Threading.Tasks;
using ApiTemplate.Factory.Users;
using ApiTemplate.Infrastructure.Extensions.ModelStates;
using ApiTemplate.Model.Commons;
using ApiTemplate.Model.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
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
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                   ModelState.GetModelStateErrors()
                );
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(   
                     ModelState.GetModelStateErrors()
                );
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
