import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ODataQueryParameters, EmailSettingModel, ATBusinessExceptionModel, SortDirection, ATPermissions } from 'at-models';
import { EmailSettingService, CommonHelperService, UserPermissionService } from 'at-services';
import { ATMenuItem, ATDetailDialogService, ATAddEditDialogService, ATSortItem, ATFilterItem } from 'at-assets';

import { EmailSettingDetailComponent } from './elements/email-setting-detail/email-setting-detail.component';
import { EmailSettingFormComponent } from './elements/email-setting-form/email-setting-form.component';
import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';

@Component({
  selector: 'at-email-setting-general',
  templateUrl: './email-setting-general.component.html'
})
export class EmailSettingGeneralComponent extends ATGridBaseComponent<EmailSettingModel> implements OnInit {

  moreActions: ATMenuItem[] = [];

  constructor(
    private _atDetailDialogService: ATDetailDialogService,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _emailSettingService: EmailSettingService,
    private _commonHelperService: CommonHelperService,
    public injector: Injector,
    private _userPermissionService: UserPermissionService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.moreActions = [
      {
        key: "edit",
        displayName: "Edit",
        icon: "edit",
        onClick: () => this._onEditItem()
      },
      {
        key: "delete",
        displayName: "Delete",
        icon: "delete",
        onClick: () => this.deleteItem("Email")
      },
      {
        key: "make_default",
        displayName: "Make Default",
        icon: "label_important",
        onClick: () => this._makeEmailSettingDefault()
      }
    ];
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isEmailSettingAddEdit) {
        this.restartGridWithPreserveState();
      }
    })
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
    this.moreActions = this.moreActions.map(x => {
      switch (x.key) {
        case "edit":
          x.isDisabled = !this.selectedItem || !this.canEdit();
        case "delete":
          x.isDisabled = !this.selectedItem || !this.canDelete();
          break;
        case "make_default":
          x.isDisabled = !this.selectedItem || !this.canEdit() || this.selectedItem.IsDefault;
          break;
      }
      return x;
    });
    this.cdf.detectChanges();
  }

  _onEditItem() {
    this._atAddEditDialogService.openAddEditDialog(EmailSettingFormComponent, this._commonHelperService.clone(this.selectedItem));
  }

  onAddClick() {
    this._atAddEditDialogService.openAddEditDialog(EmailSettingFormComponent, new EmailSettingModel);
  }

  onDoubleClick(item: EmailSettingModel) {
    this._atDetailDialogService.openDetailDialog(EmailSettingDetailComponent, item);
  }

  private _makeEmailSettingDefault() {
    if (this.selectedItem) {
      this.isContentLoading = true;
      this._emailSettingService.changeDefaultStatus(this.selectedItem.Id, true).subscribe({
        next: () => {
          this.isContentLoading = false;
          this.atSnackBarService.show("Email is made Default Successfully");
          this.restartGridWithPreserveState();
        },
        error: (error: ATBusinessExceptionModel) => {
          this.isContentLoading = false;
          this.atDialogService.openAlertDialog(this.parseError(error));
        }
      });
    }
  }

  canAdd() {
    return this.hasEmailSettingAddEditDeleteRight();
  }

  canEdit() {
    return this.hasEmailSettingAddEditDeleteRight();
  }

  canDelete() {
    return this.hasEmailSettingAddEditDeleteRight();
  }

  private hasEmailSettingAddEditDeleteRight(): boolean {
    return this._userPermissionService.hasPermission(ATPermissions.RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING);
  }

  //#region Overrides

  displayedColumns: any[] = ["Title", "Email", "SendFromName", "Host", "IsDefault", "IsPublished"];

  dataFetchAPICall(params?: ODataQueryParameters): Observable<EmailSettingModel[]> {
    return this._emailSettingService.getEmailSettings(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._emailSettingService.getCounts(params);
  }

  deleteAPICall(): Observable<boolean> {
    return this._emailSettingService.deleteEmailSetting(this.selectedItem.Id);
  }

  sortItems: ATSortItem[] = [
    { field: "IsDefault", displayName: "Default Status", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "CreatedOn", displayName: "Added Date", isSelected: false, sortDirection: SortDirection.Descending, },
    { field: "Name", displayName: "Title", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Email", displayName: "Email", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "SendFromName", displayName: "Send From Name", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Host", displayName: "Host", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "IsPublished", displayName: "Publish Status", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [];

  //#endregion
}
