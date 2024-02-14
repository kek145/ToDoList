import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IdentityEndPoints } from '../identity/IdentityEndPoints';
import { ILoginRequestModel } from 'src/models/request/ILoginRequest.model';
import { IUserResponseModel } from 'src/models/response/IUserResponse.model';
import { IBaseResponseModel } from 'src/models/response/IBaseResponse.model';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';
import { IAuthenticationResponseModel } from 'src/models/response/IAuthenticationResponse.model';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private httpClient: HttpClient) { }

  public identityLogin(login: ILoginRequestModel) : Observable<IAuthenticationResponseModel> {
    const headers = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post<IAuthenticationResponseModel>(`${environment.httpUrlApi}${IdentityEndPoints.login}`, login, { headers: headers, withCredentials: true });
  }

  public identityRegistration(registration: IRegistrationRequestModel) : Observable<IBaseResponseModel<IUserResponseModel>> {
    const headers = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post<IBaseResponseModel<IUserResponseModel>>(`${environment.httpUrlApi}${IdentityEndPoints.registration}`, registration, { headers: headers, withCredentials: true })
  }

  public identityUserId() : Observable<number> {
    const headers = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.get<number>(`${environment.httpUrlApi}api/identity/auth`, { headers: headers, withCredentials: true });
  }

  public isLogged(): boolean {
    const token = localStorage.getItem("X-Access-Token");
    return token !== null;
  }
}

