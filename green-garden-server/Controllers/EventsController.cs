using green_garden_server.Managers.Interfaces;
using green_garden_server.Models;
using green_garden_server.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IManageEvents _eventManager;

        public EventsController(IManageEvents eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventView>>> GetEvents()
        {
            return await _eventManager.GetLatestEventsAsync();
        }
    }
}
