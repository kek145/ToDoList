import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ITaskModel } from 'src/app/models/task.model';
import { Priority } from 'src/app/enums/priority.enum';

@Component({
  selector: 'app-all-task',
  templateUrl: './all-task.component.html',
  styleUrls: ['./all-task.component.css']
})
export class AllTaskComponent implements OnInit {
     
    items: ITaskModel[] = [
      { title: "Task 1", description: "Description 1", status: false, priority: Priority.Easy, createdDate: new Date(2023, 5, 3) },
      { title: "Task 2", description: "Description 2", status: false, priority: Priority.Easy, createdDate: new Date(2023, 5, 3) },
      { title: "Task 3", description: "Description 3", status: false, priority: Priority.Easy, createdDate: new Date(2023, 5, 3) },
      { title: "Task 4", description: "Description 4", status: false, priority: Priority.Easy, createdDate: new Date(2023, 5, 3) }
    ];
  constructor(private titleService: Title, private authService: AuthenticationService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.titleService.setTitle('All Tasks')
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
        this.toastr.error(`${error}`, 'Warning');
      }
    );
  }
}