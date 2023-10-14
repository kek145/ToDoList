import { Component } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import { AccountService } from './services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private auth: AuthenticationService, private account: AccountService, private router: Router) {}

  protected logout(): void {
    this.account.logout().subscribe(
      (res: any) => {
        alert(res.message);
        localStorage.clear();
        this.router.navigateByUrl('/sign-in');
      },
      (error: any) => {
        alert(`Exit error: ${JSON.stringify(error.error)}`);
      }
    );
  }

  protected isUserLogged() {
    return this.auth.isLogged();
  }
}
