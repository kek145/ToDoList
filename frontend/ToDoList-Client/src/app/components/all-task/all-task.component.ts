import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ITaskModel } from 'src/app/models/task.model';
import { Priority } from 'src/app/enums/priority.enum';
import Swal from 'sweetalert2';
import { TaskService } from 'src/app/services/task.service';

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
}