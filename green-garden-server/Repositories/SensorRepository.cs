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
    public class SensorRepository : BaseRepository, ISensorRepository
    {
        public SensorRepository(GreenGardenContext context) : base(context)
        {
        }

        public async Task AddAsync(Sensor sensor)
        {
            await _context.Sensors.AddAsync(sensor);
            await _context.Lookups
                .Include(x => x.LookupType)
                .SingleAsync(x => x.Id == sensor.SensorTypeId);
        }

        public async Task DeleteAsync(int deviceId, int sensorId)
        {
            var sensor = await GetAsync(deviceId, sensorId);
            sensor.Deleted = true;
        }

        public async Task<IEnumerable<Sensor>> GetAllAsync(int deviceId)
        {
            return await _context.Sensors
                .Include(x => x.SensorType)
                .Where(x => x.Device.Id == deviceId && !x.Deleted)
                .ToListAsync();
        }

        public async Task<Sensor> GetAsync(int deviceId, int id)
        {
            return await _context.Sensors
                .Include(x => x.SensorType)
                .Include(x => x.Device)
                .SingleAsync(x => x.DeviceId == id && x.Id == id);
        }

        public async Task UpdateAsync(Sensor sensor)
        {
            var sensorTracked = await GetAsync(sensor.DeviceId, sensor.Id);
            sensorTracked.Updated = DateTime.UtcNow;
            sensorTracked.Status = sensor.Status;
            sensorTracked.LastUpdate = DateTime.UtcNow;
        }
    }
}
