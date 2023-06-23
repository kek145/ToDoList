import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IJwtAuth } from '../models/jwtAuth.model';
import { ILoginModel } from '../models/login.model';
import { IRegisterModel } from '../models/register.model';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import Swal from 'sweetalert2';

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

  constructor(private http: HttpClient, private router: Router, private toastr: ToastrService) {
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
    this.toastr.success('you have successfully logged out!', 'Logout');
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
      // Другие действия, если необходимо
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
