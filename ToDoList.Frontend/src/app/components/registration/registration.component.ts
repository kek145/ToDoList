import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  registrationModel!: IRegistrationRequestModel;

  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
    confirmPassword: new FormControl('', Validators.required)});

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    
  }

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
      alert("ok");
    }
  }


}
