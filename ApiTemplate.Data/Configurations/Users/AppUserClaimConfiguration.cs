using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Users;

namespace RabitMQTask.Data.Configurations.Users
{
    public class AppUserClaimConfiguration   :IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            
        }
    }
}