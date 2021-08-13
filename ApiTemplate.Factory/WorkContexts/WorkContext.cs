using System.Threading.Tasks;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Entities.Users;
using ApiTemplate.Service.Users;
using Microsoft.AspNetCore.Http;

namespace ApiTemplate.Factory.WorkContexts
{
    public class WorkContext : IWorkContext, IScopedDependency
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;


        #endregion
        #region Constrcutors
        public WorkContext(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        #endregion
        #region Methods
        public AppUser CurrentUser
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User is null || !_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                return _userService.GetUserByHttpContextUser(_httpContextAccessor.HttpContext.User).GetAwaiter().GetResult();
            }
        }
        #endregion

    }
}