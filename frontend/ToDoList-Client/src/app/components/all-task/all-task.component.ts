import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-all-task',
  templateUrl: './all-task.component.html',
  styleUrls: ['./all-task.component.css']
})
export class AllTaskComponent implements OnInit {
[x: string]: any;
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
