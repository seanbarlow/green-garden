import { Lookup } from './lookup.model';

export class Sensor {

    constructor(
        public id: number,
        public deviceId: number,
        public sensorTypeId: number,
        public status: String,
        public lastUpdate: Date
    ) { };
}
