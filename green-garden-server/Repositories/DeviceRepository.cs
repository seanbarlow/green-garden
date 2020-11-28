using green_garden_server.Data;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(GreenGardenContext context) : base(context)
        {
        }

        public async Task AddEventAsync(DeviceEvent newDeviceMessage)
        {
            await _context.Events.AddAsync(newDeviceMessage);
        }

        public async Task<Device> FindByUniqueIdAsync(string deviceId)
        {
            return await _context.Devices
                .Include(d => d.Commands)
                .Include(d => d.Events)
                .Include(x => x.Sensors)
                    .ThenInclude(x => x.SensorType)
                .SingleAsync(e => e.DeviceId == deviceId);
        }

        public async Task<Command> GetNextDeviceActionAsync(string deviceId)
        {
            var device = await _context.Devices
                .Include(d => d.Commands)
                .SingleAsync(e => e.DeviceId == deviceId);
            var command = device.Commands
                .Where(x => !x.Sent)
                .OrderBy(x => x.Updated)
                .FirstOrDefault();
            return command;
        }
    }
}
