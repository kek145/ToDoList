import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITokenModel } from '../models/token.model';
import { environment } from 'src/environments/environment.development';
import { tokenEndpoints } from '../routes/token.route';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private http: HttpClient) { }

  public refreshToken() : Observable<ITokenModel> {
    return this.http.post<ITokenModel>(`${environment.httpUrlApi}${tokenEndpoints.refreshToken}`, { withCredentials: true });
  }
}
