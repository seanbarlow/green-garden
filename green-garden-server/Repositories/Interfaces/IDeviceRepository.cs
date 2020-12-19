using green_garden_server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface IDeviceRepository : IBaseRepository
    {
        Task<Device> FindByUniqueIdAsync(string deviceId);
        Task<Command> GetNextDeviceActionAsync(string deviceId, string sensorType);
        Task AddEventAsync(DeviceEvent newDeviceMessage);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device> GetAsync(int id);
        Task UpdateAsync(Device device);
        Task AddAsync(Device device);
        Task DeleteAsync(int id);
    }
}
