using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models
{
    public class DeviceEvent : BaseModel
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public string EventType { get; set; }
        public string SensorType { get; set; }
        public string ActionType { get; set; }
        public string Data { get; set; }
    }
}
