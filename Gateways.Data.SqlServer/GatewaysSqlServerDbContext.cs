using Microsoft.EntityFrameworkCore;

namespace Gateways.Data.SqlServer
{
    public class GatewaysSqlServerDbContext : GatewaysDbContext
    {
        public GatewaysSqlServerDbContext(DbContextOptions<GatewaysSqlServerDbContext> options)
            : base(options)
        {
        }
    }
}
