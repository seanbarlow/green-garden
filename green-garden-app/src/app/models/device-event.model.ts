import { Lookup } from './lookup.model';

export class DeviceEvent {
    
    constructor(
        public deviceId: string,
        public lastUpdate: string,
        public eventType: Lookup,
        public actionType: Lookup,
        public sensorType: Lookup,
        public data: Lookup
    ) {
    }
}
