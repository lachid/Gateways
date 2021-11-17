using System.Collections.Generic;

namespace Gateways.Dtos
{
    public class GatewayDto
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddressV4 { get; set; }

        public List<PeripheralDeviceDto> Devices { get; set; }
    }
}
