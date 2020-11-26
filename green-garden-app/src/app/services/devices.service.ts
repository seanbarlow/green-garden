import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Device } from '../models/device.model';
import { environment } from '../environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class DevicesService extends BaseService<Device, number> {

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/Devices`);
  }

}
