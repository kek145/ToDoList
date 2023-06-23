import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { IJwtAuth } from '../models/jwtAuth.model';
import { ILoginModel } from '../models/login.model';
import { IRegisterModel } from '../models/register.model';
import { environment } from 'src/environments/environment';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private headers: HttpHeaders;

  // Auth status
  private isAuthenticated = false;

  // Auth endpoints
  private loginUrl = "Account/LoginAccount";
  private logoutUrl = "Account/Logout";
  private registerUrl = "Account/CreateAccount";

  public isAuthenticatedResult(): boolean {
    return this.isAuthenticated;
  }

  constructor(private http: HttpClient, private router: Router) {
    this.checkAuthentication();
    this.headers = new HttpHeaders();
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

  public logout() {
    localStorage.clear();
    Swal.fire('Succesfull', 'you have successfully logged out!', 'success');
    return this.http.post(`${environment.apiUrl}/${this.logoutUrl}`, { headers: this.headers });
  }

  public register(user: IRegisterModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.registerUrl}`, user)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  private removeJwtTokenAfterDelay(delay: number) {
    setTimeout(() => {
      localStorage.removeItem('jwtToken');
    }, delay);
  }

  public login(user: ILoginModel): Observable<IJwtAuth> {
    this.isAuthenticated = true;
    return this.http.post<IJwtAuth>(`${environment.apiUrl}/${this.loginUrl}`, user)
    .pipe(
      tap((authResponse: IJwtAuth) => {
        localStorage.setItem('jwtToken', authResponse.token);
        this.removeJwtTokenAfterDelay(24 * 60 * 60 * 1000);
      }),
      catchError((error: HttpErrorResponse) => {
        this.isAuthenticated = false;
        return this.handleError(error);
      })
    );
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 400) {
      console.error('Bad Request:', error.error);
      Swal.fire('Error', `${error.error}`, 'error');
    } 
    else if (error.status === 401) {
      console.error('Unauthorized:', error.error);
      Swal.fire('Error', `${error.error}`, 'error');
      this.router.navigate(['/login']);
    } 
    else if (error.status === 500) {
      console.error('Internal Server Error:', error.error);
      Swal.fire('Error', `${error.error}`, 'error');
    } 
    else {
      console.error('An error occurred:', error);
      Swal.fire('Error', 'An error occurred!', 'error');
    }

    return throwError('Something went wrong. Please try again later.');
  }
}
