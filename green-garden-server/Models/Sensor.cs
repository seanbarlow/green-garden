using System;

namespace green_garden_server.Models
{
    public class Sensor: BaseModel
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int SensorTypeId { get; set; }
        public Lookup SensorType { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}