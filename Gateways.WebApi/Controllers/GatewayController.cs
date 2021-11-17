using Gateways.Dtos;
using Gateways.Core.Interfaces;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Gateways.WebApi.Controllers
{
    [ApiController]
    [Route("api/gateway")]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService) => _gatewayService = gatewayService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _gatewayService.GetAll());

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) => Ok(await _gatewayService.Get(id));

        [HttpPost]
        public async Task<IActionResult> Create(GatewayDto dto) => Ok(await _gatewayService.Create(dto));

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            await _gatewayService.Remove(id);
            return Ok();
        }

        [HttpPost, Route("{id}/peripheral-device")]
        public async Task<IActionResult> AddPeripheralDevice([FromRoute] int id, PeripheralDeviceDto dto) =>
            Ok(await _gatewayService.AddPeripheralDevice(id, dto));

        [HttpDelete, Route("{id}/peripheral-device/{deviceId}")]
        public async Task<IActionResult> RemovePeripheralDevice([FromRoute] int id, [FromRoute] int deviceId)
        {
            await _gatewayService.RemovePeripheralDevice(id, deviceId);
            return Ok();
        }

        [HttpGet, Route("max-devices-allowed")]
        public IActionResult MaxDevicesAllowed() => Ok(_gatewayService.MaxDevicesAllowed);
    }
}
