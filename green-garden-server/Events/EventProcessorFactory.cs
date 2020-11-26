using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Events
{
    public class EventProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EventProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProccessEvents GetEventProcessor(string sensorType)
        {

            return (IProccessEvents)_serviceProvider.GetService(typeof(EventProcessor));
            //switch (sensorType.ToLower())
            //{
            //    case "pump":
            //        return (IProccessEvents)_serviceProvider.GetService(typeof(PumpEventProcessor));
            //    case "light":
            //        return (IProccessEvents)_serviceProvider.GetService(typeof(LightEventProcessor));
            //    default:
            //        return (IProccessEvents)_serviceProvider.GetService(typeof(UnknownEventProcessor));
            //}
        }
    }
}
