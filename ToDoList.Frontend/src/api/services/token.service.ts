import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAuthenticationResponseModel } from 'src/models/response/IAuthenticationResponse.model';
import { RefreshTokenEndPoint } from '../token/RefreshTokenEndPoint';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(private httpClient: HttpClient) { }

  public refreshToken() : Observable<IAuthenticationResponseModel> {
    return this.httpClient.post<IAuthenticationResponseModel>(`${environment.httpUrlApi}${RefreshTokenEndPoint.refreshToken}`, {}, { withCredentials: true });
  }
}
