import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { SiteSettingModel, ATPermissions } from 'at-models';
import { UserPermissionService } from 'at-services';
import { ATAddEditDialogService, ATMenuItem } from 'at-assets';

import { ATGlobalStore } from '../../../../shared/services/at-global.store';
import { SiteSettingEditComponent } from './elements/site-setting-edit/site-setting-edit.component';

@Component({
  selector: 'at-site-setting-general',
  templateUrl: './site-setting-general.component.html'
})
export class SiteSettingGeneralComponent implements OnInit {

  moreActions: ATMenuItem[] = [];
  isContentLoading: boolean = false;

  siteSetting: SiteSettingModel;

  private _subscription: Subscription = new Subscription;

  constructor(
    private _atAddEditDialogService: ATAddEditDialogService,
    private _atGlobalStore: ATGlobalStore,
    private _cdf: ChangeDetectorRef,
    private _userPermissionService: UserPermissionService
  ) {
  }

  ngOnInit(): void {
    this._atGlobalStore.siteSettingSubject.subscribe(x => {
      this.siteSetting = x;
    })
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isSiteSettingEdit) {
        this._atGlobalStore.siteSetting = res.data;
      }
    });
  }

  ngAfterViewChecked(): void {
    this._cdf.detectChanges();
  }

  onEdit() {
    if (this.siteSetting) {
      this._atAddEditDialogService.openAddEditDialog(SiteSettingEditComponent, this.siteSetting);
    }
  }

  canEdit() {
    return this.hasSiteSettingEditRight();
  }

  ngOnDestroy() {
    this._subscription.unsubscribe();
  }

  private hasSiteSettingEditRight(): boolean {
    return this._userPermissionService.hasPermission(ATPermissions.RIGHT_EDIT_SITE_SETTING);
  }
}