import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from 'src/api/services/identity.service';
import { ILoginRequestModel } from 'src/models/request/ILoginRequest.model';
import { IAuthenticationResponseModel } from 'src/models/response/IAuthenticationResponse.model';

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

  constructor(private identityService: IdentityService, private router: Router) { }

  onSubmit(login: ILoginRequestModel) : void {
    if (this.registrationForm.valid) {
      this.loginModel = this.registrationForm.value as ILoginRequestModel;

      this.identityService.identityLogin(this.loginModel).subscribe({
        next: (_response: IAuthenticationResponseModel) => {
          localStorage.setItem("X-Access-Token", _response.accessToken);
          this.router.navigateByUrl('/dashboard');
        },
        error: (_error: any) => {
          alert("error");
        }
      });

    }
  }

}
