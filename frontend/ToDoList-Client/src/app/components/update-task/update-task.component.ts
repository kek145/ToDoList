import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.component.html',
  styleUrls: ['./update-task.component.css']
})
export class UpdateTaskComponent implements OnInit {
  task: ITaskModel = {
    taskId: 0,
    title: '',
    description: '',
    status: false,
    priority: Priority.Easy,
    createdDate: new Date()
  }

  constructor(private authService: AuthenticationService, private titleService: Title, private taskService: TaskService, private route: ActivatedRoute,) {
  }

  priorityOptions = [
    { value: Priority.Easy, label: 'Easy' },
    { value: Priority.Medium, label: 'Medium' },
    { value: Priority.Hard, label: 'Hard' }
  ];

  ngOnInit(): void {
    this.titleService.setTitle('Update Task');
    this.route.params.subscribe(params => {
        this.task.taskId = + params['taskId'];
        this.taskService.getTaskById(this.task.taskId).subscribe((task: ITaskModel) => {
        this.task = task;
      })
    });
  }

  updateTask(): void { 
    this.taskService.updateTask(this.task.taskId, this.task).subscribe(
      (response) => {
        console.log('Задача успешно обновлена', response);
      },
      (error) => {
        console.error('Ошибка при обновлении задачи', error);
      }
    );
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }
}
