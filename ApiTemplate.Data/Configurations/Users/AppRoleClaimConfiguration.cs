using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Data.Configurations.Users
{
    public class AppRoleClaimConfiguration:IEntityTypeConfiguration<AppRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
        {
            
        }
    }
}