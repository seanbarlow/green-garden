import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Sensor } from '../models/sensor.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class SensorsService extends BaseService<Sensor, number> {

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/Devices`);
  }

  public configureUrl(deviceId: number){
    super._base = `${environment.api.baseUrl}/Devices/${deviceId}/Sensors`;
  }

  public findSensors(): Observable<Sensor[]> {
    return this._http.get<Sensor[]>(`${this._base}`)
      .pipe(
        catchError(this.handleError)
      );

  }
}
