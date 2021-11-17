using Gateways.Core.Services.Settings;

using Gateways.Dtos;
using Gateways.Data;
using Gateways.Core.Models;
using Gateways.Core.Interfaces;
using Gateways.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace Gateways.Core.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly GatewaySettings _settings;
        private readonly GatewaysDbContext _dbContext;
        private readonly IMapper _mapper;

        public int MaxDevicesAllowed { get => _settings.MaxDeviceCount; }

        private DbSet<Gateway> Gateways { get => _dbContext.Set<Gateway>(); }

        public GatewayService(GatewaySettings settings, GatewaysDbContext dbContext, IMapper mapper) =>
            (_settings, _dbContext, _mapper) = (settings, dbContext, mapper);

        public async Task<GatewayDto> Get(int id) => _mapper.Map<GatewayDto>(await GetOrThrowException(id, readOnly: true));

        public async Task<IEnumerable<GatewayDto>> GetAll() =>_mapper.Map<IEnumerable<GatewayDto>>(await Queryable(readOnly: true).ToListAsync());

        public async Task<GatewayDto> Create(GatewayDto dto)
        {
            if (dto.Devices?.Count > _settings.MaxDeviceCount)
                throw new MaxDevicesException();

            ProcessBeforeCreate(dto);
            var entity = _mapper.Map<Gateway>(dto);

            Gateways.Add(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<GatewayDto>(entity);
        }

        public async Task Remove(int id)
        {
            var entity = await GetOrThrowException(id, loadDevices: false);
            Gateways.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PeripheralDeviceDto> AddPeripheralDevice(int id, PeripheralDeviceDto dto)
        {
            var entity = await GetOrThrowException(id);

            if (entity.DeviceCount == _settings.MaxDeviceCount)
                throw new MaxDevicesException();

            var device = AddPeripheralDevice(entity, dto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PeripheralDeviceDto>(device);
        }

        public async Task RemovePeripheralDevice(int id, int deviceId)
        {
            var entity = await GetOrThrowException(id);
            entity.RemoveDevice(deviceId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(string serialNumber)
        {
            var entity = await Queryable(readOnly: true, loadDevices: false).FirstOrDefaultAsync(e => e.SerialNumber == serialNumber);
            return entity is not null;
        }

        #region Private

        private async Task<Gateway> GetOrThrowException(int id, bool readOnly = false, bool loadDevices = true)
        {
            var entity = await Queryable(readOnly, loadDevices).FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null)
                throw new EntityNotFoundException();

            return entity;
        }

        private void ProcessBeforeCreate(GatewayDto dto)
        {
            dto.Id = default;
            dto.Devices?.ForEach(d => SetDeviceDateCreated(d));
        }

        private void SetDeviceDateCreated(PeripheralDeviceDto dto) => dto.DateCreated = DateTime.UtcNow;

        private PeripheralDevice AddPeripheralDevice(Gateway entity, PeripheralDeviceDto dto)
        {
            SetDeviceDateCreated(dto);
            var device = _mapper.Map<PeripheralDevice>(dto);
            entity.AddDevice(device);

            return device;
        }

        private IQueryable<Gateway> Queryable(bool readOnly = false, bool loadDevices = true)
        {
            var query = (readOnly) ? Gateways.AsNoTracking() : Gateways;
            if (loadDevices)
                query = query.Include(e => e.Devices);
            return query;
        }

        #endregion
    }
}
