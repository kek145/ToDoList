import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AllTaskComponent } from './components/all-task/all-task.component';
import { CreateTaskComponent } from './components/create-task/create-task.component';
import { UpdateTaskComponent } from './components/update-task/update-task.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: RegistrationComponent},
  { path: 'all-task', component: AllTaskComponent },
  { path: 'update-task/:taskId', component: UpdateTaskComponent},
  { path: 'create-task', component: CreateTaskComponent },
  { path: 'registration', component: RegistrationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
