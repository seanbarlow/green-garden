using System.Threading.Tasks;

namespace green_garden_server.Messages
{
    public interface IProcessMessages
    {
        public Task ProcessMessageAsync(DeviceMessage deviceEvent);
        public Task<DeviceAction> NextActionAsync(string deviceId);
    }
}