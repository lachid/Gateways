using Gateways.Dtos;

using System;
using System.Linq;
using System.Collections.Generic;

namespace Gateways.Tests.Fixtures
{
    internal class DeviceFixture
    {
        private static int number = 1;

        public static List<PeripheralDeviceDto> GetPeripheralDevices(int count) => Generate(count).ToList();

        private static IEnumerable<PeripheralDeviceDto> Generate(int count)
        {
            var random = new Random();
            int to = number + count;
            while (number++ < to)
                yield return new PeripheralDeviceDto
                {
                    Vendor = $"Vendor {number}",
                    Status = random.Next(2)
                };
        }
    }
}
