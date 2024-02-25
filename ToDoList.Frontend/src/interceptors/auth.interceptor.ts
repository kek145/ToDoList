import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpStatusCode
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, finalize, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { TokenService } from 'src/api/services/token.service';
import { IAuthenticationResponseModel } from 'src/models/response/IAuthenticationResponse.model';
import { IdentityService } from 'src/api/services/identity.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(private auth: IdentityService, private tokenService: TokenService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem('X-Access-Token');

    if (token) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === HttpStatusCode.Unauthorized && this.auth.isLogged()) {
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
              switchMap((jwtDto: IAuthenticationResponseModel) => {
                this.refreshTokenSubject.next(jwtDto.refreshToken);
                localStorage.setItem('X-Access-Token', jwtDto.accessToken);
                return next.handle(this.addToken(request));
              }),
              catchError((refreshError: HttpErrorResponse) => {
                if (refreshError.status === HttpStatusCode.Unauthorized || refreshError.status === HttpStatusCode.NotFound) {
                  localStorage.clear();
                  this.router.navigateByUrl('/login');
                }
                return throwError(refreshError);
              }),
              finalize(() => {
                this.isRefreshing = false;
              })
            );
          }
        }
        else {
          return throwError(error);
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>): HttpRequest<any> {
    const token = localStorage.getItem('X-Access-Token');
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
}