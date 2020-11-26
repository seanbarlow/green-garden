using green_garden_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface IDeviceRepository : IBaseRepository
    {
        Task<Device> FindByUniqueIdAsync(string deviceId);
        Task<Command> GetNextDeviceActionAsync(string deviceId);
    }
}
