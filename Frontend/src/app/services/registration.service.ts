import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IRegistrationModel } from '../models/registration.model';
import { environment } from 'src/environments/environment.development';
import { registrationEndpoints } from '../routes/registration.route';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) {}

  public registration(registerRequest: IRegistrationModel) : Observable<IRegistrationModel> {
    return this.http.post<IRegistrationModel>(`${environment.httpUrlApi}${registrationEndpoints.registration}`, registerRequest, { withCredentials: true });
  }
}
