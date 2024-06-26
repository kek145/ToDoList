import { BehaviorSubject, Observable } from 'rxjs';
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

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.isLogged());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private httpClient: HttpClient) { }

  public identityLogin(login: ILoginRequestModel) : Observable<IAuthenticationResponseModel> {
    return this.httpClient.post<IAuthenticationResponseModel>(`${environment.httpUrlApi}${IdentityEndPoints.login}`, login, { withCredentials: true });
  }

  public identityRegistration(registration: IRegistrationRequestModel) : Observable<IBaseResponseModel<IUserResponseModel>> {
    return this.httpClient.post<IBaseResponseModel<IUserResponseModel>>(`${environment.httpUrlApi}${IdentityEndPoints.registration}`, registration, { withCredentials: true })
  }

  public identityUserId() : Observable<number> {
    return this.httpClient.get<number>(`${environment.httpUrlApi}api/identity/auth`, { withCredentials: true });
  }

  setAuthenticated(isAuthenticated: boolean): void {
    this.isAuthenticatedSubject.next(isAuthenticated);
  }

  public isLogged(): boolean {
    const token = localStorage.getItem("X-Access-Token");
    return token !== null;
  }
}

