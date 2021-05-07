import { Component, OnInit } from '@angular/core';
import { ATPermissions } from 'at-models';
import { UserPermissionService } from 'at-services';

@Component({
  selector: 'at-users-layout',
  templateUrl: './users-layout.component.html'
})
export class UsersLayoutComponent implements OnInit {

  constructor(
    private _userPermissionService: UserPermissionService
  ) { }

  ngOnInit(): void {
  }

  showUserTrackRecord(): boolean {
    return this._userPermissionService.hasPermissions([ATPermissions.FEATURE_USER_TRACK_RECORD, ATPermissions.RIGHT_VIEW_USER_TRACK_RECORD]);
  }
}
