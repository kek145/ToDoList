import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';
import { IFullName } from '../../models/fullName.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  protected fullName: string = "";

  constructor(private auth: AuthenticationService, private account: AccountService, private router: Router) {}
  public ngOnInit(): void {
    const state = this.isUserLogged();
    if(state === true) {
      this.account.getFullName().subscribe(
        (res: IFullName) => {
          this.fullName = `Hi ${res.fullName}`;
        },
        (error: any) => {
          console.log(error.error);
        }
      );
    }
  }

  protected logout(): void {
    this.account.logout().subscribe(
      (res: any) => {
        console.log(res.message);
        localStorage.clear();
        this.router.navigateByUrl('/sign-in');
      },
      (error: any) => {
        console.log(`Exit error: ${JSON.stringify(error.error)}`);
      }
    );
  }

  protected isUserLogged() {
    return this.auth.isLogged();
  }
}
