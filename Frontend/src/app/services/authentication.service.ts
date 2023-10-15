import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITokenModel } from '../models/token.model';
import { ILoginModel } from '../models/authentication.model';
import { environment } from 'src/environments/environment.development';
import { authenticationEndpoints } from '../routes/authentication.route';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) {}

  public isLogged(): boolean {
    const token = this.getToken();
    return token !== null;
  }

  private getToken(): string | null {
    return localStorage.getItem("accessToken");
  }

  public authentication(authRequest: ILoginModel) : Observable<ITokenModel> {
    return this.http.post<ITokenModel>(`${environment.httpUrlApi}${authenticationEndpoints.authentication}`, authRequest, { withCredentials: true });
  }
}
