import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

import {  SessionTokenService } from 'at-services';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private _router : Router,
    private _sessionTokenService : SessionTokenService
  )
  {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
  {
    if(!this._sessionTokenService.isLoggedIn()){
      this._sessionTokenService.invalidateSession();
      this._router.navigate(['/auth'], { queryParams: { returnUrl: state.url }}); 
      return false;
    }
    return true;
  }
  
}
