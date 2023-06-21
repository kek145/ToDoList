import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IRegisterModel } from 'src/app/models/register.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  constructor(private titleService: Title) {}

  register: IRegisterModel = {
    username: '',
    email: '',
    password: '',
    confirm: ''
  };

  ngOnInit(): void {
    this.titleService.setTitle('Sign Up');
  }

  submitForm(): void {
    console.log(`username: ${this.register.username}, email: ${this.register.email}, password: ${this.register.password}, confirm: ${this.register.confirm}`);
  }
}
