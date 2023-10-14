import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IRegistrationModel } from 'src/app/models/registration.model';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  registrationRequest: IRegistrationModel = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: ''
  }


  constructor(private registrationService: RegistrationService, private router: Router) {}

  protected registration(register: IRegistrationModel): void {
    this.registrationService.registration(register).subscribe(
      (response: any) => {
        alert("ok");
        this.router.navigateByUrl("/sign-in");
      },
      (error: any) => {
        alert("Error");
      }
    );
  }
}
