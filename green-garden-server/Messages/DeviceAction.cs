using green_garden_server.Models;
using System;
using System.Linq;

namespace green_garden_server.Messages
{
    public class DeviceAction
    {
        public bool Action { get; set; }
        public string SensorType { get; set; }
        public string ActionType { get; set; }
        public int Value { get; set; }

        public static DeviceAction CreateAction(Command command)
        {
            return new DeviceAction { Action = true, ActionType = command.ActionType, SensorType = command.SensorType, Value = command.Minutes };
        }

        public static DeviceAction CreateLightsOnAction()
        {
            return new DeviceAction { Action = true, ActionType = "on", SensorType = "light", Value = 0 };
        }

        public static DeviceAction CreateLightOffAction()
        {
            return new DeviceAction { Action = true, ActionType = "off", SensorType = "light", Value = 0 };
        }
        public static DeviceAction CreateLightOnMinutessAction(int minutes)
        {
            return new DeviceAction { Action = true, ActionType = "lightonseconds", SensorType = "light", Value = minutes };
        }

        public static DeviceAction CreateNoAction()
        {
            return new DeviceAction { Action = false, ActionType = "none", SensorType = "", Value = 0 };
        }

        public static DeviceAction CreateLightOffMinutesAction(int minutes)
        {
            return new DeviceAction { Action = true, ActionType = "lightoffseconds", SensorType = "light", Value = minutes };
        }
        public static DeviceAction CreatePumpOnAction()
        {
            return new DeviceAction { Action = true, ActionType = "on", SensorType = "pump", Value = 0 };
        }
        public static DeviceAction CreatePumpOffAction()
        {
            return new DeviceAction { Action = true, ActionType = "off", SensorType = "pump", Value = 0 };
        }
        public static DeviceAction CreatePumpOnMinutesAction(int minutes)
        {
            return new DeviceAction { Action = true, ActionType = "pumponseconds", SensorType = "pump", Value = minutes };
        }
        public static DeviceAction CreatePumpOffMinutesAction(int minutes)
        {
            return new DeviceAction { Action = true, ActionType = "pumpoffseconds", SensorType = "pump", Value = minutes };
        }
    }
}
