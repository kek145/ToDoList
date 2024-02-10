import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ILoginRequestModel } from 'src/models/request/ILoginRequest.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginModel!: ILoginRequestModel;

  registrationForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)])});

  constructor() { }

  onSubmit(login: ILoginRequestModel) : void {
    if (this.registrationForm.valid) {
      this.loginModel = this.registrationForm.value as ILoginRequestModel;
    }
  }

}
