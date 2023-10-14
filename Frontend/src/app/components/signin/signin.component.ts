import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ILoginModel } from 'src/app/models/authentication.model';
import { ITokenModel } from 'src/app/models/token.model';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {
  loginRequest: ILoginModel = {
    email: "",
    password: ""
  }

  loginResponse: ITokenModel = {
    accessToken: '',
    refreshToken: ''
  };

  constructor(private authenticationService: AuthenticationService, private router: Router) {}

  protected authentication(login: ILoginModel) {
    this.authenticationService.authentication(login).subscribe((login) => {
      localStorage.setItem("jwtToken", login.accessToken);
      this.router.navigateByUrl('/dashboard');
    });
  }
}
