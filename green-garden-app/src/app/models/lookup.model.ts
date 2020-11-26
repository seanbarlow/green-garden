import { LookupType } from './lookup-type.model';

export class Lookup {
    constructor(
        public id: number,
        public lookupType: LookupType,
        public name: string,
        public description: string
    ) { };
}
