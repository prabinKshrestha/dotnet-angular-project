import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { ATPermissions } from 'at-models';
import { UserPermissionService } from 'at-services';

@Injectable({
    providedIn: 'root'
})
export class UserTrackRecordGuard implements CanActivate {

    constructor(
        private _router: Router,
        private _userPermissionService: UserPermissionService
    ) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (this._userPermissionService.hasPermissions([ATPermissions.FEATURE_USER_TRACK_RECORD, ATPermissions.RIGHT_VIEW_USER_TRACK_RECORD])) {
            return true;
        }
        this._router.navigate(['/']);
        return false;
    }
}