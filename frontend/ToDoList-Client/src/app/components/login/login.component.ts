import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ILoginModel } from 'src/app/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private titleService: Title) {}

  login: ILoginModel = {
    email: '',
    password: ''
  }

  ngOnInit(): void {
    this.titleService.setTitle('Sign In');
  }
}
