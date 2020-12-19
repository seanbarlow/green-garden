using green_garden_server.Data;
using green_garden_server.Managers.Interfaces;
using green_garden_server.Models;
using green_garden_server.Repositories.Interfaces;
using green_garden_server.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Managers
{
    public class EventManager : IManageEvents
    {
        private readonly ILookupManager _lookupManager;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMessageRepository _messageRepository;
        public EventManager(ILookupManager lookupManager, 
            IDeviceRepository deviceRepository, 
            IMessageRepository messageRepository)
        {
            _lookupManager = lookupManager;
            _deviceRepository = deviceRepository;
            _messageRepository = messageRepository;
        }

        public Task CheckForAnamoliesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventView>> GetLatestEventsAsync()
        {
            var devices = await GetDevicesAsync();
            var latestEvents = new List<EventView>();
            foreach(var device in devices)
            {
                latestEvents.AddRange(await GetLastEventAsync(device.Id));
            }
            return latestEvents;
        }
        
        private async Task<IEnumerable<EventView>> GetLastEventAsync(int deviceId)
        {
            var deviceEvents = await _messageRepository.GetLastMessagesAsync(deviceId);
            var latestEvents = new List<EventView>();
            foreach (var deviceEvent in deviceEvents)
            {
                var eventType = await _lookupManager.GetAsync("eventtype", deviceEvent.EventType);
                var actionType = await _lookupManager.GetAsync("actionType", deviceEvent.EventType);
                var sensorType = await _lookupManager.GetAsync("sensorType", deviceEvent.EventType);
                var lastEventView = new EventView
                {
                    DeviceId = deviceEvent.Device.DeviceId,
                    LastUpdate = deviceEvent.Created.ConvertTimeFromUtc(),
                    EventType = eventType,
                    ActionType = actionType,
                    SensorType = sensorType,
                    Data = deviceEvent.Data
                };
                latestEvents.Add(lastEventView);
            }
            return latestEvents;
        }

        private async Task<IEnumerable<Device>> GetDevicesAsync()
        {
            return await _deviceRepository.GetAllAsync();
        }
    }
}
