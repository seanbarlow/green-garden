using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Messages
{
    public class MessageProcessor : IProcessMessages
    {

        protected readonly ILookupRepository _lookupRepository;
        protected readonly IDeviceRepository _deviceRepository;

        public MessageProcessor(IDeviceRepository deviceRepository,
            ILookupRepository lookupRepository)
        {
            this._deviceRepository = deviceRepository;
            this._lookupRepository = lookupRepository;
        }
        public async Task<DeviceAction> NextActionAsync(string deviceId)
        {
            var command = await this._deviceRepository.GetNextDeviceActionAsync(deviceId);

            if (command != null)
            {
                return DeviceAction.CreateAction(command);
            }
            return DeviceAction.CreateNoAction();
        }

        public async Task ProcessMessageAsync(DeviceMessage deviceMessage)
        {
            var device = await _deviceRepository.FindByUniqueIdAsync(deviceMessage.DeviceId);
            // get the sensor using the device unique id
            var sensorType = await _lookupRepository.GetLookupAsync("sensortypes", deviceMessage.SensorType);
            // determine what type of Message?
            var eventType = await _lookupRepository.GetLookupAsync("eventtypes", deviceMessage.EventType);
            // update the sensor
            var sensors = device.Sensors.ToList();
            var sensor = sensors.First(x => x.SensorTypeId == sensorType.Id);
            sensor.LastUpdate = DateTime.UtcNow;
            sensor.Status = eventType.Description;

            var actionType = await _lookupRepository.GetLookupAsync("actiontypes", deviceMessage.ActionType);

            await SaveDeviceMessage(device, sensor, eventType, actionType, deviceMessage.Data);
        }

        protected async Task SaveDeviceMessage(Device device, Sensor sensor, Lookup eventType, Lookup actionType, string data)
        {
            var newDeviceMessage = new DeviceEvent
            {
                DeviceId = device.Id,
                EventType = eventType.Name,
                SensorType = sensor.SensorType.Name,
                ActionType = actionType.Name,
                Data = data
            };
            await _deviceRepository.AddEventAsync(newDeviceMessage);
            await _deviceRepository.SaveAsync();
        }
    }
}