using Gateways.Dtos;
using Gateways.Core.Models;

using AutoMapper;

namespace Gateways.WebApi.Mappings
{
    public class GatewayMappingProfile : Profile
    {
        public GatewayMappingProfile()
        {
            CreateMap<Gateway, GatewayDto>()
                .ReverseMap()
                .ForMember(e => e.Id, c => c.Ignore());

            CreateMap<PeripheralDevice, PeripheralDeviceDto>()
                .ReverseMap()
                .ForMember(e => e.Id, c => c.Ignore());
        }
    }
}
