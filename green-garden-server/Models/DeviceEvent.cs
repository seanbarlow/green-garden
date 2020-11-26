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
        public Lookup EventType { get; set; }
        public int EventTypeId { get; set; }
        public Lookup SensorType { get; set; }
        public int SensorTypeId { get; set; }
        public Lookup ActionType { get; set; }
        public int ActionTypeId { get; set; }
        public string Data { get; set; }
    }
}
