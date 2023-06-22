import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ITaskModel } from '../models/task.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  
  // Task endpoints
  private createTaskUrl = "Task/CreateTask";
  private getTaskUrl = "Task/GetAllTask";
  private updateTaskUrl = "Task/UpdateTask/{taskId}";
  private deleteTaskUrl = "Task/DeleteTask/{taskId}";

  constructor(private http: HttpClient) { }

  public createTask(task: ITaskModel): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/${this.createTaskUrl}`, task);
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
}
