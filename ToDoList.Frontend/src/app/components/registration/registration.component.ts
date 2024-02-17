import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { HttpStatusCode } from '@angular/common/http';
import {ModalComponent} from "../modal/modal.component";
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IdentityService } from 'src/api/services/identity.service';
import { IUserResponseModel } from 'src/models/response/IUserResponse.model';
import { IBaseResponseModel } from 'src/models/response/IBaseResponse.model';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  registrationModel!: IRegistrationRequestModel;
  loading: boolean = false;

  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl('', Validators.required)});

  constructor(private identityService: IdentityService, private dialog: MatDialog, private router: Router) { }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.loading = true; // Показать загрузку перед запросом
      this.registrationModel = this.registrationForm.value as IRegistrationRequestModel;
      this.identityService.identityRegistration(this.registrationModel).subscribe({
        next: (_response: IBaseResponseModel<IUserResponseModel>) => {
          if (_response.statusCode === HttpStatusCode.Created) {
            this.dialog.open(ModalComponent, {
              width: '550px',
              height: '350px',
              data: { status: 'Success', message: `${_response.message}` }
            });
            this.router.navigateByUrl('/login');
          }
          else {
            alert(_response.message);
          }
          this.loading = false;
        },
        error: (_error: any) => {
          if(_error.error.statusCode === HttpStatusCode.BadRequest) {
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
            this.router.navigateByUrl('/500');
          }
          this.loading = false;
        }
      });
    }
  }

}
