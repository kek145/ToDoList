import { Component } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  constructor(private identityService: IdentityService) {}

  protected isUserLogged(): boolean {
    return this.identityService.isLogged();
  }

  protected logout(): void {
    localStorage.clear();
  }
}
