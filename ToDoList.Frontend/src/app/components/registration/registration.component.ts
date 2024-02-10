import { Component } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { IUserResponseModel } from 'src/models/response/IUserResponse.model';
import { IBaseResponseModel } from 'src/models/response/IBaseResponse.model';
import { HttpStatusCode } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { Route, Router } from '@angular/router';
import { SuccessModalComponent } from '../success-modal/success-modal.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  registrationModel!: IRegistrationRequestModel;

  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl('', Validators.required)});

  constructor(private identityService: IdentityService, private dialog: MatDialog, private router: Router) { }

  public validatePassword(control: FormControl) {
    const pattern = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
    const valid = pattern.test(control.value);
    return valid ? null : { invalidPassword: true };
  }
  

  public passwordsMatchValidator(formGroup: FormControl) {
    const password = formGroup.get('password')!.value;
    const confirmPassword = formGroup.get('confirmPassword')!.value;

    return password === confirmPassword ? null : { passwordsMismatch: true };
  }
  

  onSubmit() {
    if (this.registrationForm.valid) {
      this.registrationModel = this.registrationForm.value as IRegistrationRequestModel;
      this.identityService.identityRegistration(this.registrationModel).subscribe({
        next: (_response: IBaseResponseModel<IUserResponseModel>) => {
          if (_response.statusCode === HttpStatusCode.Created) {
            const dialogRef = this.dialog.open(SuccessModalComponent, {
              width: '550px',
              height: '350px',
              data: { message: `${_response.message}` }
            });
          } 
          else {
            alert(_response.message);
          }
        },
        error: (_error: any) => {
          if(_error.error.statusCode === HttpStatusCode.BadRequest) {
            const dialogRef = this.dialog.open(ErrorModalComponent, {
              width: '550px',
              height: '350px',
              data: { message: `${_error.error.errors[0]}` }
            });
        
            dialogRef.afterClosed().subscribe((_result: any) => {
              console.log('The error modal was closed');
            });
          }
          else {
            this.router.navigateByUrl('500');
          }
        }
      });
    }
  }
  
}