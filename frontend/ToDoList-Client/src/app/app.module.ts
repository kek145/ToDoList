import { NgModule } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { AuthenticationInterceptor } from './services/interceptor';
import { LoginComponent } from './components/login/login.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { AuthenticationService } from './services/authentication.service';
import { AllTaskComponent } from './components/all-task/all-task.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreateTaskComponent } from './components/create-task/create-task.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UpdateTaskComponent } from './components/update-task/update-task.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AllTaskComponent,
    UpdateTaskComponent,
    CreateTaskComponent,
    RegistrationComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  providers: [AuthenticationService, DatePipe, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
})
export class AppModule { }
