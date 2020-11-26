using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models
{
    public class Command : BaseModel
    {
        public int DeviceId { get; set; }
        public Device Device { get;set; }
        public int SensorTypeId { get; set; }
        public Lookup SensorType { get; set; }
        public Lookup ActionType { get; set; }
        public int ActionTypeId { get; set; }
        public int Minutes { get; set; }
        public bool Sent { get; set; }
    }
}
