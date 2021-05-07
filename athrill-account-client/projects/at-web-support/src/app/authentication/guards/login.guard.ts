import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SessionTokenService } from 'projects/at-services/src/public-api';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {

  constructor(
    private _router : Router,
    private _sessionTokenService : SessionTokenService
  )
  {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
  {
    if(this._sessionTokenService.isLoggedIn()){
      this._router.navigate(['/']); 
      return false;
    }
    return true;
  }
}
