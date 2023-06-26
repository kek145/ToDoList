import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IJwtAuth } from 'src/app/models/jwtAuth.model';
import { IRegisterModel } from 'src/app/models/register.model';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private authService: AuthenticationService, private titleService: Title) { }


  registerDto: IRegisterModel = {
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
  };

  jwtDto: IJwtAuth = {
    token: ''
  }

  ngOnInit(): void {
    this.titleService.setTitle('Sign Up');
  }

  register(registerDto: IRegisterModel) {
    if(this.registerDto.username === "" || this.registerDto.email === "" || this.registerDto.password === "" || this.registerDto.confirmPassword === "") {
      Swal.fire('Warning', 'All fields must be filled!', 'warning');
      return;
    }

    if(this.registerDto.password != this.registerDto.confirmPassword) {
      Swal.fire('Warning', 'Password mismatch!', 'warning');
      return;
    }

    this.authService.register(registerDto).subscribe(
      response => {
        Swal.fire('Successfully', 'Registration completed successfully!', 'success');
      },
      error => {
        Swal.fire('Error', 'Error registration', 'error');
      }
    );
  }
}
