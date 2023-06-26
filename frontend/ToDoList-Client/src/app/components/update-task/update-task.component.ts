import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-task',
  templateUrl: './update-task.component.html',
  styleUrls: ['./update-task.component.css']
})
export class UpdateTaskComponent implements OnInit {
  taskDto: ITaskModel = {
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
        this.taskDto.taskId = + params['taskId'];
        this.taskService.getTaskById(this.taskDto.taskId).subscribe((task: ITaskModel) => {
        this.taskDto = task;
      })
    });
  }

  updateTask(taskDto: ITaskModel): void {
    this.taskService.updateTask(taskDto.taskId, taskDto).subscribe(
      (response) => {
        Swal.fire('Successfully', 'Task updated successfully', 'success');
      },
      (error) => {
        Swal.fire('Error', '', 'error');
        console.error('Error while updating task', error);
      }
    );
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }
}
