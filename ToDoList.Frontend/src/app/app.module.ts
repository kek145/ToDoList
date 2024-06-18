import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import { PriorityPipe } from './pipes/priority.pipe';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { MaterialModule } from 'src/material/material.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AuthInterceptor } from 'src/interceptors/auth.interceptor';
import { ModalComponent } from './components/modal/modal.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LoaderComponent } from './components/loader/loader.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CreateUpdateComponent } from './components/create-update/create-update.component';
import { StatusTranslatePipe } from './pipes/statusTranslate.pipe';
import { InternalServerErrorComponent } from './components/internal-server-error/internal-server-error.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    ModalComponent,
    HeaderComponent,
    FooterComponent,
    LoaderComponent,
    DashboardComponent,
    InternalServerErrorComponent,
    RegistrationComponent,
    PriorityPipe,
    StatusTranslatePipe,
    CreateUpdateComponent,
  ],
  imports: [
    FormsModule,
    CommonModule,
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
