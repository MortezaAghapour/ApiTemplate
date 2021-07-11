using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Data.Configurations.Users
{
    public class AppRoleConfiguration   :IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            
        }
    }
}