import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { DeviceEvent } from '../models/device-event.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class EventsService extends BaseService<DeviceEvent, number> {

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/Events`);
  }

}