import { Router } from '@angular/router';
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
  constructor(private titleService: Title, private authService: AuthenticationService, private router: Router) {}

  ngOnInit(): void {
    this.titleService.setTitle('All Tasks')
  }

  get isAuthenticated(): boolean {
    return this.authService.isAuthenticatedResult();
  }
}
