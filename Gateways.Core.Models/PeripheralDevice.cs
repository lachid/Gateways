using System;

namespace Gateways.Core.Models
{
    public class PeripheralDevice : IEntity
    {
        public int Id { get; set; }

        public int GatewayId { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public PeripheralDeviceStatus Status { get; set; }
    }
}
