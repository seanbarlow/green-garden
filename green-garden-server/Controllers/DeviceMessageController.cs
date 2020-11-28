using green_garden_server.Messages;

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
    public class DeviceMessageController : ControllerBase
    {
        private readonly IProcessMessages _messageProcessor;

        public DeviceMessageController(IProcessMessages messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }
       // POST api/<ActionsController>
        [HttpPost]
        public async Task<DeviceAction> Post(DeviceMessage deviceEvent)
        {
            await _messageProcessor.ProcessMessageAsync(deviceEvent);
            return await _messageProcessor.NextActionAsync(deviceEvent.DeviceId);
        }
    }
}
