
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TaskService } from 'src/app/services/task.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {
  constructor(private titleService: Title, private authService: AuthenticationService, private taskService: TaskService, private toastr: ToastrService) { }

  taskDto: ITaskModel = {
    title: '',
    description: '',
    status: false,
    priority:  Priority.Easy,
    createdDate: new Date()
  };

  priorityOptions = [
    { value: Priority.Easy, label: 'Easy' },
    { value: Priority.Medium, label: 'Medium' },
    { value: Priority.Hard, label: 'Hard' }
  ];

  ngOnInit(): void {
    this.titleService.setTitle('Create Task');
  }

  createTask(taskDto: ITaskModel) {
    if(this.taskDto.title === "" || this.taskDto.description === "") {
      Swal.fire('Warning', 'All fields must be filled!', 'warning');
      return;
    }

    this.taskService.createTask(taskDto).subscribe(
      response => {
        console.log(response);
        Swal.fire('Ok', 'Task created successfully', 'success');
      }
    );
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }
}
