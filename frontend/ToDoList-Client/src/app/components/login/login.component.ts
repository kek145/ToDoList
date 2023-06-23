import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IJwtAuth } from 'src/app/models/jwtAuth.model';
import { ILoginModel } from 'src/app/models/login.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthenticationService, private titleService: Title, private router: Router) {}

  loginDto: ILoginModel = {
    email: '',
    password: ''
  }

  jwtDto: IJwtAuth = {
    token: ''
  }

  ngOnInit(): void {
    this.titleService.setTitle('Sign In');
  }

  login(loginDto: ILoginModel): void {
    if(loginDto.email === "" || loginDto.password === "") {
      Swal.fire('Warning', 'All fields must be filled!', 'warning');
      return;
    }

    this.authService.login(loginDto).subscribe((jwtDto) => {
      localStorage.setItem('jwtToken', jwtDto.token);
      this.router.navigate(['/all-task']);
    });
  }
}
