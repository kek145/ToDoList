import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Priority } from 'src/app/enums/priority.enum';
import { ITaskModel } from 'src/app/models/task.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {
  constructor(private titleService: Title, private authService: AuthenticationService, private taskService: TaskService, private datePipe: DatePipe, private toastr: ToastrService) { }

  taskDto: ITaskModel = {
    title: '',
    description: '',
    status: false,
    priority: Priority.easy,
    createdDate: new Date()
  };

  ngOnInit(): void {
    this.titleService.setTitle('Create Task');
  }

  createTask(taskDto: ITaskModel) {
    if(this.taskDto.title === "" || this.taskDto.description === "") {
      this.toastr.warning('All fields must be filled!', 'Warning');
      return;
    }


    this.taskService.createTask(taskDto).subscribe(
      response => {
        this.toastr.success('Task added successfully', 'Successfully');
      },
      error => {
        this.toastr.error(`${error.message}`, 'Error');
      }
    );
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }
}
