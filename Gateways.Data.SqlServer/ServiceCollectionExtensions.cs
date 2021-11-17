using Gateways.Data;
using Gateways.Data.SqlServer;

using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GatewaysSqlServerDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<GatewaysDbContext, GatewaysSqlServerDbContext>();

            return services;
        }
    }
}
