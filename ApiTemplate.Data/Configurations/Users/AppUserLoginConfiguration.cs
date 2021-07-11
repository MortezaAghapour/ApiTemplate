using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Data.Configurations.Users
{
    public class AppUserLoginConfiguration   :IEntityTypeConfiguration<AppUserLogin>
    {
        public void Configure(EntityTypeBuilder<AppUserLogin> builder)
        {
            
        }
    }
}