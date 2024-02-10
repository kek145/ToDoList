import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IAuthenticationResponseModel } from 'src/models/response/IAuthenticationResponse.model';
import { IdentityEndPoints } from '../identity/IdentityEndPoints';
import { ILoginRequestModel } from 'src/models/request/ILoginRequest.model';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';
import { IUserResponseModel } from 'src/models/response/IUserResponse.model';
import { IBaseResponseModel } from 'src/models/response/IBaseResponse.model';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private httpClient: HttpClient) { }

  public identityLogin(login: ILoginRequestModel) : Observable<IAuthenticationResponseModel> {
    return this.httpClient.post<IAuthenticationResponseModel>(`${environment.httpUrlApi}${IdentityEndPoints.login}`, login, { withCredentials: true });
  }

  public identityRegistration(registration: IRegistrationRequestModel) : Observable<IBaseResponseModel<IUserResponseModel>> {
    return this.httpClient.post<IBaseResponseModel<IUserResponseModel>>(`${environment.httpUrlApi}${IdentityEndPoints.registration}`, registration, { withCredentials: true })
  }
}
