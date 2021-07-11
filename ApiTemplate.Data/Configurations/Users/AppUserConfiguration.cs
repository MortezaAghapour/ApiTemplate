using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Data.Configurations.Users
{
    public class AppUserConfiguration :IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            
        }
    }
}