import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { ATPermissions } from 'at-models';
import { UserPermissionService } from 'at-services';

@Injectable({
    providedIn: 'root'
})
export class SiteSettingGuard implements CanActivate {

    constructor(
        private _router: Router,
        private _userPermissionService: UserPermissionService
    ) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (this._userPermissionService.hasPermissions([ATPermissions.FEATURE_SITE_SETTING, ATPermissions.RIGHT_VIEW_SITE_SETTING])) {
            return true;
        }
        this._router.navigate(['/']);
        return false;
    }
}

@Injectable({
    providedIn: 'root'
})
export class EmailSettingGuard implements CanActivate {

    constructor(
        private _router: Router,
        private _userPermissionService: UserPermissionService
    ) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (this._userPermissionService.hasPermissions([ATPermissions.FEATURE_EMAIL_SETTING, ATPermissions.RIGHT_VIEW_EMAIL_SETTING])) {
            return true;
        }
        this._router.navigate(['/']);
        return false;
    }
}


@Injectable({
    providedIn: 'root'
})
export class UserGuard implements CanActivate {

    constructor(
        private _router: Router,
        private _userPermissionService: UserPermissionService
    ) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (this._userPermissionService.hasPermissions([ATPermissions.FEATURE_USER, ATPermissions.RIGHT_VIEW_USER])) {
            return true;
        }
        this._router.navigate(['/']);
        return false;
    }
}
