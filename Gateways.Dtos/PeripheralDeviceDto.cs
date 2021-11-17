using System;

namespace Gateways.Dtos
{
    public class PeripheralDeviceDto
    {
        public int Id { get; set; }

        public int GatewayId { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public int Status { get; set; }
    }
}
