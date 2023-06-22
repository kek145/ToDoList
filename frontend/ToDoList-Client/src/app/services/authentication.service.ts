import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IJwtAuth } from '../models/jwtAuth.model';
import { ILoginModel } from '../models/login.model';
import { IRegisterModel } from '../models/register.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // Auth status
  private isAuthenticated = false;

  public isAuthenticatedResult(): boolean {
    return this.isAuthenticated;
  }

  // Auth endpoints
  loginUrl = "Auth/LoginAccount";
  registerUrl = "Auth/CreateAccount";

  // Task endpoints
  createTaskUrl = "Task/CreateTask";
  getTaskUrl = "Task/GetAllTask";
  updateTaskUrl = "Task/UpdateTask/{taskId}";
  deleteTaskUrl = "Task/DeleteTask/{taskId}";

  constructor(private http: HttpClient) {
    this.checkAuthentication();
  }

  private checkAuthentication(): void {
    const token = localStorage.getItem('jwtToken'); // Получение токена из localStorage (может потребоваться проверка и в sessionStorage)

    if (token) {
      this.isAuthenticated = true;
    } 
    else {
      this.isAuthenticated = false;
    }
  }

  public register(user: IRegisterModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.registerUrl}`, user);
  }

  public login(user: ILoginModel): Observable<IJwtAuth> {
    this.isAuthenticated = true;
    return this.http.post<IJwtAuth>(`${environment.apiUrl}/${this.loginUrl}`, user);
  }
}
