import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HttpStatusCode } from '@angular/common/http';
import {ModalComponent} from "../modal/modal.component";
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  loading = false;

  registrationForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)])});

  constructor(private identityService: IdentityService, private dialog: MatDialog, private router: Router) { }

  onSubmit(login: ILoginRequestModel) : void {
    if (this.registrationForm.valid) {
      this.loginModel = this.registrationForm.value as ILoginRequestModel;

      this.loading = true;

      this.identityService.identityLogin(this.loginModel).subscribe({
        next: (_response: IAuthenticationResponseModel) => {
          localStorage.setItem("X-Access-Token", _response.accessToken);
          this.loading = false;
          this.router.navigateByUrl('/dashboard');
        },
        error: (_error: any) => {
          if(_error.error.statusCode === HttpStatusCode.Unauthorized) {
            const dialogRef = this.dialog.open(ModalComponent, {
              width: '550px',
              height: '350px',
              data: { status: 'Error', message: `${_error.error.errors[0]}` }
            });

            dialogRef.afterClosed().subscribe((_result: any) => {
              console.log('The error modal was closed');
            });
          }
          else {
            this.router.navigateByUrl('500');
          }
          this.loading = false;
        }
      });

    }
  }

}
