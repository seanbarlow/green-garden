using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models
{
    public class Device : BaseModel
    {
        public string DeviceId { get; set; }
        public ICollection<DeviceEvent> Events { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
        public ICollection<Command> Commands { get; set; }
    }
}


