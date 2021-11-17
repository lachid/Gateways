using System.Collections.Generic;
using System.Linq;

namespace Gateways.Core.Models
{
    public class Gateway : IEntity
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddressV4 { get; set; }

        public int DeviceCount { get => Devices.Count; }

        public ICollection<PeripheralDevice> Devices { get; set; } = new List<PeripheralDevice>();
    }

    public static class GatewayExtensions
    {
        public static void AddDevice(this Gateway @this, PeripheralDevice device) => @this.Devices.Add(device);

        public static void AddDevices(this Gateway @this, IEnumerable<PeripheralDevice> devices)
        {
            foreach (var device in devices)
                @this.AddDevice(device);
        }

        public static void RemoveDevice(this Gateway @this, int deviceId)
        {
            var device = @this.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device is not null)
                @this.Devices.Remove(device);
        }
    }
}
