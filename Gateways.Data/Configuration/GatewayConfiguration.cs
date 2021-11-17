using Gateways.Core.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateways.Data.Configuration
{
    public class GatewayConfiguration : GatewaysEntityTypeConfiguration<Gateway>
    {
        public override void Configure(EntityTypeBuilder<Gateway> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.SerialNumber).IsUnique();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(15).IsRequired();

            builder.HasMany(e => e.Devices);

            builder.Ignore(e => e.DeviceCount);
        }
    }
}
