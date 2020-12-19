using green_garden_server.Messages;
using green_garden_server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace green_garden_server.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<DeviceEvent>> GetLastMessagesAsync(int deviceId);
    }
}