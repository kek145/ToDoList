import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IJwtAuth } from '../models/jwtAuth.model';
import { ILoginModel } from '../models/login.model';
import { IRegisterModel } from '../models/register.model';
import { Observable, catchError, map, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // Auth status
  private isAuthenticated = false;

  // Auth endpoints
  private loginUrl = "Account/LoginAccount";
  private registerUrl = "Account/CreateAccount";

  public isAuthenticatedResult(): boolean {
    return this.isAuthenticated;
  }

  constructor(private http: HttpClient, private router: Router, private toastr: ToastrService) {
    this.checkAuthentication();
  }

  private checkAuthentication(): void {
    const token = localStorage.getItem('jwtToken');

    if (token) {
      this.isAuthenticated = true;
    } 
    else {
      this.router.navigate(['/login']);
      this.isAuthenticated = false;
    }
  }

  public register(user: IRegisterModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.registerUrl}`, user)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  public login(user: ILoginModel): Observable<IJwtAuth> {
    this.isAuthenticated = true;
    return this.http.post<IJwtAuth>(`${environment.apiUrl}/${this.loginUrl}`, user)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 400) {
      console.error('Bad Request:', error.error);
      this.toastr.error(`${error.error}`, 'Error');
    } 
    else if (error.status === 401) {
      console.error('Unauthorized:', error.error);
      this.toastr.error(`${error.error}`, 'Error');
      this.router.navigate(['/login']);
    } 
    else if (error.status === 500) {
      console.error('Internal Server Error:', error.error);
      this.toastr.error(`${error.error}`, 'Error');
    } 
    else {
      console.error('An error occurred:', error);
      this.toastr.error('An error occurred', 'Error');
    }

    return throwError('Something went wrong. Please try again later.');
  }
}
