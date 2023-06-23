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
  
  // Task endpoints
  private createTaskUrl = "Task/CreateTask";
  private getTaskUrl = "Task/GetAllTask";
  private updateTaskUrl = "Task/UpdateTask/${taskId}";
  private deleteTaskUrl = "Task/DeleteTask/${taskId}";

  constructor(private http: HttpClient, private router: Router) { }

  public createTask(task: ITaskModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.createTaskUrl}`, task)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        Swal.fire('Error', 'Task not created!', 'error');
        return this.handleError(error);
      })
    );;
  }

  public getTask(): Observable<ITaskModel[]> {
    return this.http.get<ITaskModel[]>(`${environment.apiUrl}/${this.getTaskUrl}`);
  }

  public updateTask(taskId: number, task: ITaskModel): Observable<any> {
    return this.http.put<any>(`${environment.apiUrl}/${this.updateTaskUrl}/${taskId}`, task);
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/Task/DeleteTask/${taskId}`);
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
