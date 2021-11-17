using Gateways.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateways.Core.Interfaces
{
    public interface IGatewayService : IService
    {
        int MaxDevicesAllowed { get; }

        Task<IEnumerable<GatewayDto>> GetAll();

        Task<GatewayDto> Get(int id);

        Task<GatewayDto> Create(GatewayDto dto);

        Task Remove(int id);

        Task<PeripheralDeviceDto> AddPeripheralDevice(int id, PeripheralDeviceDto dto);

        Task RemovePeripheralDevice(int id, int deviceId);

        Task<bool> Exists(string serialNumber);
    }
}
