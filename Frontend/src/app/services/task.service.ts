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

  public createTask(taskRequest: ITaskModel): Observable<ITaskModel> {
    return this.http.post<ITaskModel>(`${environment.httpUrlApi}${taskEndpoints.createTask}`, taskRequest, { withCredentials: true });
  }

  public updateTask(taskId: number, taskRequest: ITaskModel) {
    return this.http.put(`${environment.httpUrlApi}${taskEndpoints.updateTask}${taskId}`, taskRequest, { withCredentials: true });
  }
  
  public completedTask(taskId: number): Observable<any> {
    return this.http.patch(`${environment.httpUrlApi}${taskEndpoints.completeTask}/${taskId}`, {}, { withCredentials: true });
  }

  public getTaskById(taskId: number) : Observable<ITaskModel> {
    return this.http.get<ITaskModel>(`${environment.httpUrlApi}${taskEndpoints.taskById}/${taskId}`, { withCredentials: true });
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

  public searchTask(page: number, search: string): Observable<{ items: ITaskModel[], currentPage: number, pages: number }> {
    return this.http.get<{ items: ITaskModel[], currentPage: number, pages: number }>(`${environment.httpUrlApi}${taskEndpoints.searchTask}?search=${search}&page=${page}`, { withCredentials: true });
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(`${environment.httpUrlApi}${taskEndpoints.deleteTask}/${taskId}`,  { withCredentials: true });
  }

}
