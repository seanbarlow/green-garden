import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Lookup } from '../models/lookup.model';
import { LookupType } from '../models/lookup-type.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class LookupTypeService extends BaseService<LookupType, number> {

  constructor(protected _http: HttpClient) {
    super(_http, `${environment.api.baseUrl}/LookupTypes`);
  }

  public findAllByUniqueId(uniqueId: string): Observable<Lookup[]> {
    return this._http.get<Lookup[]>(`${this._base}/${uniqueId}/Lookups`)
      .pipe(
        catchError(this.handleError)
      );

  }
}
