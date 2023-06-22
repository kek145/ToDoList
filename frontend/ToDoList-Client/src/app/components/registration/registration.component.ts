import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IJwtAuth } from 'src/app/models/jwtAuth.model';
import { IRegisterModel } from 'src/app/models/register.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  constructor(private authService: AuthenticationService, private titleService: Title, private toastr: ToastrService) {  }

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
      this.toastr.warning('All fields must be filled!', 'Warning');
      return;
    }

    if(this.registerDto.password != this.registerDto.confirmPassword) {
      this.toastr.warning('Password mismatch!', 'Warning');
      return;
    }

    this.authService.register(registerDto).subscribe(
      response => {
        this.toastr.success('Registration completed successfully!', 'Successfully');
      },
      error => {
        this.toastr.error('Registration failed!', 'Error');
      }
    );
  }
}
