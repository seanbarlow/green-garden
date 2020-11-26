﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Events
{
    public class DeviceEvent
    {
        public string DeviceId { get; set; }
        public string EventType { get; set; }
        public string SensorType { get; set; }
        public string ActionType { get; set; }
        public string Data { get; set; }

    }
}