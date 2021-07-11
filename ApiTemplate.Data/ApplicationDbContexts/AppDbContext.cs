using ApiTemplate.Core.Entities.Base;
using ApiTemplate.Core.Entities.Users;
using ApiTemplate.Data.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Data.ApplicationDbContexts
{
    public class AppDbContext :IdentityDbContext<AppUser,AppRole,long,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>
    {
     
        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var assemblies = typeof(IEntity).Assembly;

            //register all entities
            builder.RegisterEntities<IEntity>(assemblies);
            //register all configurations
            builder.RegisterConfigurations(GetType().Assembly);
            //add restrict delete behavior convention
            builder.AddRestrictDeleteBehaviorConvention();
            //pluralize all table names
            builder.PluralizingTableNameConvention();
        }
    }
}
