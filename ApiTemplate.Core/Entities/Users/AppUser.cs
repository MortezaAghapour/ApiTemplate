using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RabitMQTask.Core.Entities.Base;

namespace RabitMQTask.Core.Entities.Users
{
    public class AppUser   :IdentityUser<long>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
