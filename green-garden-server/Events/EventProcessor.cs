using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Events
{
    public class EventProcessor : IProccessEvents
    {

        protected readonly ILookupRepository _lookupRepository;
        protected readonly IDeviceRepository _deviceRepository;

        public EventProcessor(IDeviceRepository deviceRepository,
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

        public async Task ProcessEventAsync(DeviceEvent deviceEvent)
        {
            var device = await _deviceRepository.FindByUniqueIdAsync(deviceEvent.DeviceId);
            // get the sensor using the device unique id
            var sensorType = await _lookupRepository.GetLookupAsync("sensorType", deviceEvent.SensorType);
            // determine what type of event?
            var eventType = await _lookupRepository.GetLookupAsync("eventtype", deviceEvent.EventType);
            // update the sensor
            var sensors = device.Sensors.ToList();
            var sensor = sensors.First(x => x.SensorTypeId == sensorType.Id);
            sensor.LastUpdate = DateTime.Now;
            sensor.Status = eventType.Description;

            var actionType = await _lookupRepository.GetLookupAsync("actiontype", deviceEvent.ActionType);

            await SaveDeviceEvent(device, sensor, eventType, actionType, deviceEvent.Data);
        }

        protected async Task SaveDeviceEvent(Device device, Sensor sensor, Lookup eventType, Lookup actionType, string data)
        {
            var newDeviceEvent = new Models.DeviceEvent
            {

                EventTypeId = eventType.Id,
                SensorTypeId = sensor.SensorTypeId,
                ActionTypeId = actionType.Id,
                Data = data
            };
            device.Events.Add(newDeviceEvent);
            await _deviceRepository.SaveAsync();
        }
    }
}