using Gateways.Core.Exceptions;
using Gateways.Core.Interfaces;
using Gateways.Core.Models;
using Gateways.Data;
using Gateways.Tests.Fixtures;

using System.Threading.Tasks;
using System.Linq;

using Xunit;

namespace Gateways.Tests
{
    public class GatewayServiceTests
    {
        private readonly Setup _setup;
        private readonly IGatewayService _gatewayService;

        private GatewaysDbContext DBContext { get => _setup.DBContext; }

        public GatewayServiceTests()
        {
            _setup = new Setup();
            _gatewayService = _setup.GetService<IGatewayService>();
        }

        [Fact]
        public async Task MaxDeviceCapacityExceptionOnGatewayCreationTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithMaxDevices();
            var device = DeviceFixture.GetPeripheralDevices(1).First();
            gateway.Devices.Add(device);

            // Act and Assert
            await Assert.ThrowsAsync<MaxDevicesException>(async () => await _gatewayService.Create(gateway));
        }

        [Fact]
        public async Task OkGatewayCreationTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();

            // Act
            var savedGateway = await _gatewayService.Create(gateway);

            // Assert
            var savedGateways = DBContext.Set<Gateway>().ToList();
            Assert.Single(savedGateways);

            Assert.True(
                (gateway.Name, gateway.SerialNumber, gateway.IpAddressV4) == (savedGateway.Name, savedGateway.SerialNumber, savedGateway.IpAddressV4));
        }

        [Fact]
        public async Task MaxDeviceCapacityExceptionOnDeviceAddTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithMaxDevices();
            var savedGateway = await _gatewayService.Create(gateway);
            var device = DeviceFixture.GetPeripheralDevices(1).First();

            // Act and Assert
            await Assert.ThrowsAsync<MaxDevicesException>(async () =>
                await _gatewayService.AddPeripheralDevice(savedGateway.Id, device));
        }

        [Fact]
        public async Task EntityNotFoundExceptionOnDeviceAddTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);
            var device = DeviceFixture.GetPeripheralDevices(1).First();

            int invalidGatewayId = savedGateway.Id + 1;

            // Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await _gatewayService.AddPeripheralDevice(invalidGatewayId, device));
        }

        [Fact]
        public async Task OkAddDeviceToGatewayTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);
            var device = DeviceFixture.GetPeripheralDevices(1).First();

            // Act
            await _gatewayService.AddPeripheralDevice(savedGateway.Id, device);

            // Assert
            var savedDevices = DBContext.Set<PeripheralDevice>().ToList();
            Assert.True(savedDevices.Count == 2);

            Assert.True(savedDevices.All(device => device.GatewayId == savedGateway.Id));
        }

        [Fact]
        public async Task EntityNotFoundExceptionOnGatewayRemoveTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);

            int invalidGatewayId = savedGateway.Id + 1;

            // Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _gatewayService.Remove(invalidGatewayId));
        }

        [Fact]
        public async Task OkRemoveGatewayTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);

            // Act
            await _gatewayService.Remove(savedGateway.Id);

            // Assert
            var savedGateways = DBContext.Set<Gateway>().ToList();
            Assert.Empty(savedGateways);
        }

        [Fact]
        public async Task EntityNotFoundExceptionOnRemoveDeviceFromGatewayTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);

            int invalidGatewayId = savedGateway.Id + 1;
            int deviceId = savedGateway.Devices.First().Id;

            // Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _gatewayService.RemovePeripheralDevice(invalidGatewayId, deviceId));
        }

        [Fact]
        public async Task OkRemoveDeviceFromGatewayTest()
        {
            _setup.CleanData();

            // Arrange
            var gateway = GatewayFixture.GetGatewayWithOneDevice();
            var savedGateway = await _gatewayService.Create(gateway);

            int deviceId = savedGateway.Devices.First().Id;

            // Act
            await _gatewayService.RemovePeripheralDevice(savedGateway.Id, deviceId);

            // Assert
            var savedDevices = DBContext.Set<PeripheralDevice>().ToList();
            Assert.Empty(savedDevices);
        }
    }
}
