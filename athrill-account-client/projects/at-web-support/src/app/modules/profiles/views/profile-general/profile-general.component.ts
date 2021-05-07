import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';

import { ATAddEditDialogService, ATMenuItem } from 'at-assets';
import { UserModel } from 'at-models';
import { ATGlobalStore } from '../../../../shared/services/at-global.store';
import { ProfileEditComponent } from './elements/profile-edit/profile-edit..component';
import { ProfileChangePasswordComponent } from './elements/profile-change-password/profile-change-password.component';

@Component({
  selector: 'at-profile-general',
  templateUrl: './profile-general.component.html'
})
export class ProfileGeneralComponent implements OnInit {

  moreActions: ATMenuItem[] = [];
  isContentLoading: boolean = false;

  user: UserModel;

  private _subscription: Subscription = new Subscription;
  private _routeState: Observable<any>;

  constructor(
    public _activatedRoute: ActivatedRoute,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _atGlobalStore: ATGlobalStore,
    private _cdf: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    // *** Start
    // This block is for getting route state which is navigated from other component.
    // We are sending isChangePassword from login component so that we can open dialog for change password.
    this._routeState = this._activatedRoute.paramMap.pipe(map(() => window.history.state));
    this._routeState.subscribe(res => {
      if (res && res.isChangePassword) {
        this.onChangePassword();
      }
    })
    // *** End
    this._atGlobalStore.userInformationSubject.subscribe(x => {
      this.user = x;
    })
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isProfileEdit) {
        this._atGlobalStore.userInformation = res.data;
      }
    });
  }

  ngAfterViewChecked(): void {
    this._cdf.detectChanges();
  }

  onEdit() {
    if (this.user) {
      this._atAddEditDialogService.openAddEditDialog(ProfileEditComponent, this.user);
    }
  }

  onChangePassword() {
      this._atAddEditDialogService.openAddEditDialog(ProfileChangePasswordComponent, null);
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }
}
