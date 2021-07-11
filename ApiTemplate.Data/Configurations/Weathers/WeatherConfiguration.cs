using ApiTemplate.Core.Entities.Weathers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTemplate.Data.Configurations.Weathers
{
    public class WeatherConfiguration  :IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.Property(c => c.City).HasMaxLength(250);
        }
    }
}