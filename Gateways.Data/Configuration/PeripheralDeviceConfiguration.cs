using Gateways.Core.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gateways.Data.Configuration
{
    public class PeripheralDeviceConfiguration : GatewaysEntityTypeConfiguration<PeripheralDevice>
    {
        public override void Configure(EntityTypeBuilder<PeripheralDevice> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.GatewayId);
        }
    }
}
