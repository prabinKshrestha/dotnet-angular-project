import { Component, OnInit } from '@angular/core';
import { ATDialogService } from 'at-assets';
import { ATBusinessExceptionModel, ODataQueryParameters, UserModel } from 'at-models';
import { CommonHelperService, UserContextService, UserService } from 'at-services';

import { ATGlobalStore } from '../../shared/services/at-global.store';

@Component({
  selector: 'at-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.scss']
})
export class CoreComponent implements OnInit {

  isContentLoading: boolean = false;
  user: UserModel;

  constructor(
    private _userContextService: UserContextService,
    private _userService: UserService,
    private _atGlobalStore: ATGlobalStore,
    private _commonHelperService: CommonHelperService,
    private _atDialogService: ATDialogService,
  ) { }

  ngOnInit(): void {
    this._atGlobalStore.userInformationSubject.subscribe(x => {
      this.user = x;
    });
    this._getUserInformation();
  }

  private _getUserInformation() {
    this.isContentLoading = true;
    let oDataParams: ODataQueryParameters = new ODataQueryParameters;
    oDataParams.Expand = "UserLogin, UserRoleLink.UserRole, Gender";
    this._userService.getUser(this._userContextService.get().Id, oDataParams).subscribe({
      next: (res) => {
        this.isContentLoading = false;
        this._atGlobalStore.userInformation = res;
      },
      error: (error: ATBusinessExceptionModel) => {
        this.isContentLoading = false;
        this._atDialogService.openAlertDialog(this._commonHelperService.parseError(error));
      }
    });
  }

}
