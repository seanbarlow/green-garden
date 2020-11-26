using System.Threading.Tasks;

namespace green_garden_server.Events
{
    public interface IProccessEvents
    {
        public Task ProcessEventAsync(DeviceEvent deviceEvent);
        public Task<DeviceAction> NextActionAsync(string deviceId);
    }
}