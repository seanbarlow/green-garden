import { Lookup } from './lookup.model';

export class Log {
    constructor(
        public id: number,
        public deviceId: string,
        public message: string,
        public messageType: Lookup,
        public data: string,
        public dataType: Lookup,
        public created: string,
        public updated: string) { }
}
