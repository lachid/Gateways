using Gateways.Dtos;

using System.Collections.Generic;

namespace Gateways.Tests.Fixtures
{
    internal class GatewayFixture
    {
        public static GatewayDto GetGatewayWithOneDevice() => new GatewayDto
        {
            Name = "Gateway 1",
            SerialNumber = "SN 1",
            IpAddressV4 = "1.1.1.1",
            Devices = DeviceFixture.GetPeripheralDevices(1)
        };

        public static GatewayDto GetGatewayWithMaxDevices() => new GatewayDto
        {
            Name = "Gateway 2",
            SerialNumber = "SN 2",
            IpAddressV4 = "2.2.2.2",
            Devices = DeviceFixture.GetPeripheralDevices(GatewaySettingsFixture.GetGatewaySettings().MaxDeviceCount)
        };
    }
}
