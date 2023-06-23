import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ITaskModel } from 'src/app/models/task.model';
import { TaskService } from 'src/app/services/task.service';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-all-task',
  templateUrl: './all-task.component.html',
  styleUrls: ['./all-task.component.css']
})
export class AllTaskComponent implements OnInit {
     
  items: ITaskModel[] = [];
  constructor(private titleService: Title, private authService: AuthenticationService, private router: Router, private taskService: TaskService) {}

  ngOnInit(): void {
    this.titleService.setTitle('All Tasks');

    this.taskService.getTask().subscribe(
      (response: ITaskModel[]) => {
        this.items = response;
      },
      (error: any) => {
        console.log(error)
      }
    );
  }
  getPriorityText(priority: number): string {
    switch (priority) {
      case 1:
        return 'Easy';
      case 2:
        return 'Medium';
      case 3:
        return 'Hard';
      default:
        return '';
    }
  }
  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }

  onLogout() {
    this.authService.logout().subscribe(
      () => {
        this.router.navigate(['/login']);
      },
      (error) => {
        Swal.fire('Error', `${error}`, 'error');
      }
    );
  }

  removeTask(taskId: number) {
    this.taskService.deleteTask(taskId).subscribe(
      (response) => {
        Swal.fire('Accesfull', `${response}`, 'success');
        this.taskService.getTask().subscribe(
          (response: ITaskModel[]) => {
            this.items = response;
          },
          (error: any) => {
            console.log(error)
          }
        );
      },
      (error) => {
        Swal.fire('Error', `${error}`, 'error');
      }
    );
  }
}