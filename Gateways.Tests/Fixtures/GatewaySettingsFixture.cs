using Gateways.Core.Services.Settings;

namespace Gateways.Tests.Fixtures
{
    internal class GatewaySettingsFixture
    {
        public static GatewaySettings GetGatewaySettings() => new GatewaySettings { MaxDeviceCount = 10 };
    }
}
