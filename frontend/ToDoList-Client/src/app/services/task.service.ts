import { Observable, catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { ITaskModel } from '../models/task.model';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  
  // Task endpoints
  private createTaskUrl = "Task/CreateTask";
  private getTaskUrl = "Task/GetAllTask";
  private updateTaskUrl = "Task/UpdateTask/{taskId}";
  private deleteTaskUrl = "Task/DeleteTask/{taskId}";

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) { }

  public createTask(task: ITaskModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.createTaskUrl}`, task)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        this.toastr.error('Task not created!', 'Error');
        return this.handleError(error);
      })
    );;
  }

  public getTask(): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/${this.getTaskUrl}`);
  }

  public updateTask(taskId: number, task: ITaskModel): Observable<any> {
    return this.http.put<any>(`${environment.apiUrl}/${this.updateTaskUrl}/${taskId}`, task);
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/${this.deleteTaskUrl}/${taskId}`);
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 401) {
      console.error('Unauthorized:', error.error);
      this.toastr.error(`${error.error}`, 'Error');
      this.router.navigate(['/login']);
    } 
    return throwError('Something went wrong. Please try again later.');
  }
}
