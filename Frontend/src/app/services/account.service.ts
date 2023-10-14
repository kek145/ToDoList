import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { accountEndpoints } from '../routes/account.route';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) {}

  public logout(): Observable<any> {
    return this.http.delete(`${environment.httpUrlApi}${accountEndpoints.logout}`, { withCredentials: true });
  }
}
