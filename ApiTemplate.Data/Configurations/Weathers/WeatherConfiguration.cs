using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabitMQTask.Core.Entities.Weathers;

namespace RabitMQTask.Data.Configurations.Weathers
{
    public class WeatherConfiguration  :IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.Property(c => c.City).HasMaxLength(250);
        }
    }
}