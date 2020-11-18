using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}


