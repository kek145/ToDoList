import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITaskModel } from '../models/task.model';
import { taskEndpoints } from '../routes/task.route';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient) {}
  
  public completedTask(taskId: number): Observable<any> {
    return this.http.patch(`${environment.httpUrlApi}${taskEndpoints.completeTask}/${taskId}`, { withCredentials: true });
  }

  public getAllTasks(page: number): Observable<{ items: ITaskModel[], currentPage: number, pages: number }> {
    return this.http.get<{ items: ITaskModel[], currentPage: number, pages: number }>(`${environment.httpUrlApi}${taskEndpoints.allTasks}?page=${page}`, { withCredentials: true });
  }

  public getFailedTasks(page: number): Observable<{ items: ITaskModel[], currentPage: number, pages: number }> {
    return this.http.get<{ items: ITaskModel[], currentPage: number, pages: number }>(`${environment.httpUrlApi}${taskEndpoints.failedTasks}?page=${page}`, { withCredentials: true });
  }

  public getCompletedTasks(page: number): Observable<{ items: ITaskModel[], currentPage: number, pages: number }> {
    return this.http.get<{ items: ITaskModel[], currentPage: number, pages: number }>(`${environment.httpUrlApi}${taskEndpoints.completedTasks}?page=${page}`, { withCredentials: true });
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${environment.httpUrlApi}${taskEndpoints.deleteTask}/${taskId}`, { withCredentials: true });
  }
}
