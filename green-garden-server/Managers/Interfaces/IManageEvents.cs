using green_garden_server.Messages;
using green_garden_server.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Managers.Interfaces
{
    public interface IManageEvents
    {
        Task<List<EventView>> GetLatestEventsAsync();
        Task CheckForAnamoliesAsync();
    }
}
