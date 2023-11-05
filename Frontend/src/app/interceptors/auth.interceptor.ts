import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { 
  BehaviorSubject,
   Observable,
    catchError,
     finalize,
      switchMap,
       throwError 
      } from 'rxjs';
import { Router } from '@angular/router';
import { ITokenModel } from '../models/token.model';
import { TokenService } from '../services/token.service';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private auth: AuthenticationService, private tokenService: TokenService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('accessToken');

    if (token) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401 && this.auth.isLogged()) {
          if (this.isRefreshing) {
            return this.refreshTokenSubject.pipe(
              switchMap(() => {
                return next.handle(this.addToken(request));
              }),
              finalize(() => {
                this.isRefreshing = false;
              })
            );
          } 
          else {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            return this.tokenService.refreshToken().pipe(
              switchMap((jwtDto: ITokenModel) => {
                this.refreshTokenSubject.next(jwtDto.refreshToken);
                localStorage.setItem('accessToken', jwtDto.accessToken);
                return next.handle(this.addToken(request));
              }),
              catchError((refreshError: HttpErrorResponse) => {
                if (refreshError.status === 401) {
                  localStorage.clear();
                  this.router.navigateByUrl('/sign-in');
                }
                return throwError(refreshError);
              }),
              finalize(() => {
                this.isRefreshing = false;
              })
            );
          }
        } else {
          return throwError(error);
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>): HttpRequest<any> {
    const token = localStorage.getItem('accessToken');
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
}
