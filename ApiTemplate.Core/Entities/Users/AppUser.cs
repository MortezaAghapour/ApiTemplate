using Microsoft.AspNetCore.Identity;

namespace ApiTemplate.Core.Entities.Users
{
    public class AppUser   :IdentityUser<long>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
