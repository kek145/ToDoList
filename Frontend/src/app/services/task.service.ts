import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITaskModel } from '../models/task.model';
import { environment } from 'src/environments/environment.development';
import { taskEndpoints } from '../routes/task.route';
import { ITaskListModel } from '../models/taskList.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient) {}
  
  public getAllTasks(page: number): Observable<{ items: ITaskModel[], currentPage: number, pages: number }> {
    return this.http.get<{ items: ITaskModel[], currentPage: number, pages: number }>(`${environment.httpUrlApi}${taskEndpoints.allTasks}?page=${page}`);
  }
}
