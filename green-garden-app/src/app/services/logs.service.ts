import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Log } from '../models/log.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class LogsService extends BaseService<Log, number> {

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/bookmarks`);
  }

}
