using Gateways.Data;
using Gateways.Data.SqlServer;
using Gateways.Core.Interfaces;
using Gateways.Core.Services;
using Gateways.Core.Services.Settings;
using Gateways.WebApi;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Tests
{
    internal class Setup
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GatewaysDbContext _dbContext;

        public GatewaysDbContext DBContext { get => _dbContext; }

        public TService GetService<TService>() => _serviceProvider.GetService<TService>();

        public Setup()
        {
            var services = InitializeServices();
            _serviceProvider = services.BuildServiceProvider();
            _dbContext = _serviceProvider.GetService<GatewaysDbContext>();
        }

        public void CleanData()
        {
            _dbContext.Database.EnsureDeleted();
        }

        private IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            AddDbContext(services);
            AddSettings(services);
            AddMapperProfiles(services);
            AddAppServices(services);

            return services;
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<GatewaysSqlServerDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("TestDB");
                opt.EnableSensitiveDataLogging();
            });
            services.AddScoped<GatewaysDbContext, GatewaysSqlServerDbContext>();
        }

        private void AddSettings(IServiceCollection services)
        {
            var gatewaySettings = new GatewaySettings { MaxDeviceCount = 10 };
            services.AddSingleton(gatewaySettings);
        }

        private void AddMapperProfiles(IServiceCollection services)
        {
            var startupAssembly = typeof(Startup).Assembly;
            services.AddAutoMapper(startupAssembly);
        }

        private void AddAppServices(IServiceCollection services)
        {
            services.AddScoped<IGatewayService, GatewayService>();
        }
    }
}
