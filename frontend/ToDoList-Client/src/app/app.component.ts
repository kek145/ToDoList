import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ToDoList-Client';
  constructor(private authService: AuthenticationService, private router: Router) {}

  ngOnInit() {
    if (this.authService.isAuthenticatedResult()){
      this.router.navigate(['all-task']);
    }
  }
}
