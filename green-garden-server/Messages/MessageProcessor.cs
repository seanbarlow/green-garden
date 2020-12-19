using green_garden_server.Managers.Interfaces;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Messages
{
    public class MessageProcessor : IProcessMessages
    {

        protected readonly ILookupManager _lookupManager;
        protected readonly IDeviceRepository _deviceRepository;

        public MessageProcessor(IDeviceRepository deviceRepository,
            ILookupManager lookupManager)
        {
            this._deviceRepository = deviceRepository;
            this._lookupManager = lookupManager;
        }
        public async Task<DeviceAction> NextActionAsync(string deviceId, string sensorType)
        {
            var command = await this._deviceRepository.GetNextDeviceActionAsync(deviceId, sensorType);

            if (command != null)
            {
                command.Sent = true;
                command.Updated = DateTime.UtcNow;
                await this._deviceRepository.SaveAsync();
                return DeviceAction.CreateAction(command);
            }
            return DeviceAction.CreateNoAction();
        }

        public async Task ProcessMessageAsync(DeviceMessage deviceMessage)
        {
            var device = await _deviceRepository.FindByUniqueIdAsync(deviceMessage.DeviceId);
            // get the sensor using the device unique id
            var sensorType = await _lookupManager.GetAsync("sensortypes", deviceMessage.SensorType);
            // determine what type of Message?
            var eventType = await _lookupManager.GetAsync("eventtypes", deviceMessage.EventType);
            // update the sensor
            var sensors = device.Sensors.ToList();
            var sensor = sensors.First(x => x.SensorTypeId == sensorType.Id);
            sensor.LastUpdate = DateTime.UtcNow;
            sensor.Status = eventType.Description;

            var actionType = await _lookupManager.GetAsync("actiontypes", deviceMessage.ActionType);

            await SaveDeviceMessage(device, sensor, eventType, actionType, deviceMessage.Data);
        }

        protected async Task SaveDeviceMessage(Device device, Sensor sensor, Lookup eventType, Lookup actionType, string data)
        {
            var newDeviceMessage = new DeviceEvent
            {
                DeviceId = device.Id,
                EventType = eventType.UniqueId,
                SensorType = sensor.SensorType.UniqueId,
                ActionType = actionType.UniqueId,
                Data = data
            };
            await _deviceRepository.AddEventAsync(newDeviceMessage);
            await _deviceRepository.SaveAsync();
        }
    }
}