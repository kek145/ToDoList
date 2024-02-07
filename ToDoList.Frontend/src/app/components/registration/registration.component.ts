import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { IRegistrationRequestModel } from 'src/models/request/IRegistrationRequest.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  registrationModel: IRegistrationRequestModel = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: ''
  };


  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
  });

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  passwordsMatchValidator(formGroup: FormGroup) {
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
