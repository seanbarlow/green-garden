import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

export abstract class BaseService<T, ID>  {

  constructor(
    protected _http: HttpClient,
    protected _base: string
  ) { }

  save(t: T): Observable<T> {
    return this._http.post<T>(this._base, t)
      .pipe(
        catchError(this.handleError)
      );
  }

  update(id: ID, t: T): Observable<T> {
    return this._http.put<T>(this._base + "/" + id, t, {})
      .pipe(
        catchError(this.handleError)
      );
  }

  findOne(id: ID): Observable<T> {
    return this._http.get<T>(this._base + "/" + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  findAll(): Observable<T[]> {
    return this._http.get<T[]>(this._base)
      .pipe(
        catchError(this.handleError)
      );
  }

  delete(id: ID): Observable<T> {
    return this._http.delete<T>(this._base + '/' + id)
      .pipe(
        catchError(this.handleError)
      );
  }

  handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}