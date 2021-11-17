using Gateways.Data.Configuration;

using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace Gateways.Data
{
    public abstract class GatewaysDbContext : DbContext
    {
        public GatewaysDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityConfigurationTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type =>
                    !type.IsAbstract
                    && type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(GatewaysEntityTypeConfiguration<>));

            foreach (var configurationType in entityConfigurationTypes)
            {
                dynamic configurationInstance = Activator.CreateInstance(configurationType);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class DbContextExtensions
    {
        public static void ApplyPendingMigrations(this GatewaysDbContext dbContext) => dbContext.Database.Migrate();
    }
}
