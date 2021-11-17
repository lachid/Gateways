using Gateways.Dtos;
using Gateways.Core.Interfaces;

using System.Collections.Generic;

using FluentValidation;

namespace Gateways.WebApi.Validators
{
    public class GatewayValidator : AbstractValidator<GatewayDto>
    {
        public GatewayValidator(IGatewayService gatewayService)
        {
            ValidateGatewayRequiredFields();
            ValidateDuplicateGateway(gatewayService);
        }

        private void ValidateGatewayRequiredFields()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage($"Gateway {_displayingLabels[nameof(GatewayDto.Name)]} can not be empty.");

            RuleFor(e => e.SerialNumber)
                .NotEmpty()
                .WithMessage($"Gateway {_displayingLabels[nameof(GatewayDto.SerialNumber)]} can not be empty.");

            RuleFor(e => e.IpAddressV4)
                .Custom((ipAddress, context) =>
                {
                    if (ipAddress?.IsValidIpv4() != true)
                        context.AddFailure($"Gateway {_displayingLabels[nameof(GatewayDto.IpAddressV4)]} most be valid IPv4 format.");
                });
        }

        private void ValidateDuplicateGateway(IGatewayService gatewayService)
        {
            RuleFor(e => e.SerialNumber)
                .CustomAsync(async (serialNumber, context, cancelationToken) =>
                {
                    if (await gatewayService.Exists(serialNumber))
                        context.AddFailure($"Gateway with {_displayingLabels[nameof(GatewayDto.SerialNumber)]} {serialNumber} already exists.");
                });
        }

        private Dictionary<string, string> _displayingLabels = new Dictionary<string, string>
        {
            { nameof(GatewayDto.Name), "Name" },
            { nameof(GatewayDto.SerialNumber), "Serial Number" },
            { nameof(GatewayDto.IpAddressV4), "Ip Address" },
        };
    }

    #region Extensions Helpers

    internal static class IpAddressExtensions
    {
        internal static bool IsValidIpv4(this string ipAddress)
        {
            var parts = ipAddress.Split('.');
            if (parts.Length != 4)
                return false;

            foreach (var part in parts)
                if (!(int.TryParse(part, out int ipPart) && ipPart.IsInRange(0, 255)))
                    return false;

            return true;
        }

        internal static bool IsInRange(this int number, int left, int right) => number >= left && number <= right;
    }

    #endregion
}
