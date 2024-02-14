import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})

export class AuthGuardService implements CanActivate {
  constructor(private auth: IdentityService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if(this.auth.isLogged()) {
      return true;
    }
    else {
      this.router.navigateByUrl("/login");
      return false;
    }
  }
  
}
