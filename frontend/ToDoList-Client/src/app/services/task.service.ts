import { Observable, catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { ITaskModel } from '../models/task.model';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private http: HttpClient, private router: Router) { }

  public createTask(task: ITaskModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/Task/CreateTask`, task)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        Swal.fire('Error', 'Task not created!', 'error');
        return this.handleError(error);
      })
    );;
  }

  public getTask(): Observable<ITaskModel[]> {
    return this.http.get<ITaskModel[]>(`${environment.apiUrl}/Task/GetAllTask`);
  }

  public getTaskById(taskId: number) : Observable<ITaskModel> {
    return this.http.get<ITaskModel>(`${environment.apiUrl}/Task/GetTaskById/${taskId}`);
  }

  public updateTask(taskId: number, task: ITaskModel): Observable<any> {
    return this.http.put<any>(`${environment.apiUrl}/Task/UpdateTask/${taskId}`, task);
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/Task/DeleteTask/${taskId}`);
  }
  public endTask(taskId: number): Observable<any> {
    return this.http.patch(`${environment.apiUrl}/Task/CompleteTask/${taskId}`, null);
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 401) {
      console.error('Unauthorized:', error.error);
      Swal.fire('Error', `${error.error}`, 'error');
      this.router.navigate(['/login']);
    } 
    return throwError('Something went wrong. Please try again later.');
  }
}
