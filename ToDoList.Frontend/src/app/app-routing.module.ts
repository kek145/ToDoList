import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { InternalServerErrorComponent } from './components/internal-server-error/internal-server-error.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuardService } from 'src/guards/auth.guard';
import {NoteComponent} from "./components/note/note.component";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '500', component: InternalServerErrorComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'create', component: NoteComponent, canActivate: [AuthGuardService] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
