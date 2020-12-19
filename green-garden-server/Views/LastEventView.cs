using green_garden_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Views
{
    public class EventView
    {
        public string DeviceId { get; internal set; }
        public DateTime LastUpdate { get; internal set; }
        public Lookup EventType { get; internal set; }
        public Lookup ActionType { get; internal set; }
        public Lookup SensorType { get; internal set; }
        public string Data { get; internal set; }
    }
}
