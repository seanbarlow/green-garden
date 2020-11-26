using green_garden_server.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace green_garden_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventProcessor _eventProcessor;

        public EventsController(EventProcessor eventProcessor)
        {
            _eventProcessor = eventProcessor;
        }
       // POST api/<ActionsController>
        [HttpPost]
        public async Task<DeviceAction> Post(DeviceEvent deviceEvent)
        {
            await _eventProcessor.ProcessEventAsync(deviceEvent);
            return await _eventProcessor.NextActionAsync(deviceEvent.DeviceId);
        }
    }
}
