import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Lookup } from '../models/lookup.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class LookupService extends BaseService<Lookup, number> {
  public lookups: Lookup[] = [];

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/lookups`);
  }

}
