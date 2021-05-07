import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ODataQueryParameters, SortDirection, UserModel, UserRegistrationModel, ATCustomDataType, ATDataTypes, ATDataValueModel, UserRoleModel, ATPermissions, UserEditModel } from 'at-models';
import { ATDatasService, UserPermissionService, UserService } from 'at-services';
import { ATMenuItem, ATDetailDialogService, ATAddEditDialogService, ATSortItem, ATFilterItem } from 'at-assets';

import { UserDetailComponent } from './elements/user-detail/user-detail.component';
import { UserRegistrationComponent } from './elements/user-registration/user-registration.component';
import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';
import { UserEditComponent } from './elements/user-edit/user-edit.component';

@Component({
  selector: 'at-user-general',
  templateUrl: './user-general.component.html'
})
export class UserGeneralComponent extends ATGridBaseComponent<UserModel> implements OnInit {

  moreActions: ATMenuItem[] = [];

  constructor(
    private _atDetailDialogService: ATDetailDialogService,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _userService: UserService,
    private _atDataService: ATDatasService,
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
        onClick: () => this.onEditItem()
      },
      {
        key: "delete",
        displayName: "Delete",
        icon: "delete",
        onClick: () => this.deleteItem("User")
      }
    ];
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isUserRegistration) {
        this.restartGridWithPreserveState();
      }
    })
    this._atAddEditDialogService.dialogClosed.subscribe(res =>{
      if (res && res.isRegisteredUserUpdated) {
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
      }
      return x;
    });
    this.cdf.detectChanges();
  }

  onEditItem() {
    this.selectedItems[0];
    let passVal : UserEditModel = new UserEditModel;
    passVal.IsActive = this.selectedItems[0].IsActive;
    passVal.UserRoleId = this.selectedItems[0].UserRoleLink.UserRoleId;
    passVal.UserId = this .selectedItems[0].UserId;
    this._atAddEditDialogService.openAddEditDialog(UserEditComponent, passVal);
  }

  onRegisterUserClick() {
    this._atAddEditDialogService.openAddEditDialog(UserRegistrationComponent, new UserRegistrationModel);
  }

  onDoubleClick(item: UserModel) {
    this._atDetailDialogService.openDetailDialog(UserDetailComponent, item);
  }

  canAdd() {
    return this._hasEmailSettingAddEditDeleteRight();
  }

  canEdit() {
    return this._hasEmailSettingAddEditDeleteRight();
  }

  canDelete() {
    return this._hasEmailSettingAddEditDeleteRight();
  }

  private _hasEmailSettingAddEditDeleteRight(): boolean {
    return this._userPermissionService.hasPermission(ATPermissions.RIGHT_ADD_EDIT_DELETE_USER);
  }

  filterGenderDataFetchFn(): Observable<ATDataValueModel[]> {
    return this._atDataService.getATDataValuesByType(ATDataTypes.Gender);
  }
  filterGenderDisplayWithFn(value: ATDataValueModel) {
    return value?.DisplayName;
  }
  filterGenderValueFn(value: ATDataValueModel) {
    return value?.Id;
  }

  filterUserRoleDataFetchFn(): Observable<UserRoleModel[]> {
    return this._userService.getUserRoles();
  }
  filterUserRoleDisplayWithFn(value: UserRoleModel) {
    return value?.DisplayName;
  }
  filterUserRoleValueFn(value: UserRoleModel) {
    return value?.UserRoleId;
  }
  //#region Overrides

  displayedColumns: any[] = ["Image", "FullName", "Email", "PhoneNumber", "Role", "IsActive"];

  dataFetchAPICall(params?: ODataQueryParameters): Observable<UserModel[]> {
    return this._userService.getUsers(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._userService.getCounts(params);
  }

  setExpandODataParams(): string {
    return "UserRoleLink.UserRole, Gender, UserLogin"
  }

  deleteAPICall(): Observable<boolean> {
    return this._userService.deleteUser(this.selectedItem.Id);
  }

  sortItems: ATSortItem[] = [
    { field: "CreatedOn", displayName: "Added Date", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "UpdatedOn", displayName: "Updated Date", isSelected: false, sortDirection: SortDirection.Descending, },
    { field: "FirstName", displayName: "First Name", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Email", displayName: "Email", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "UserRoleLink.UserRole.DisplayName", displayName: "Role", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Gender.DisplayName", displayName: "Gender", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "IsActive", displayName: "Active Status", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [
    { field: "FirstName", displayName: "First Name", dataType: ATCustomDataType.String },
    { field: "LastName", displayName: "Last Name", dataType: ATCustomDataType.String },
    { field: "Email", displayName: "Email", dataType: ATCustomDataType.String },
    {
      field: "GenderId",
      displayName: "Gender",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterGenderDataFetchFn(),
        displayFn: this.filterGenderDisplayWithFn.bind(this),
        dataValueFn: this.filterGenderValueFn.bind(this)
      }
    },
    {
      field: "UserRoleLink.UserRoleId",
      displayName: "Role",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterUserRoleDataFetchFn(),
        displayFn: this.filterUserRoleDisplayWithFn.bind(this),
        dataValueFn: this.filterUserRoleValueFn.bind(this)
      }
    },
    { field: "DOB", displayName: "DOB", dataType: ATCustomDataType.Date, isUTCDate : true },
    { field: "IsActive", displayName: "Active Status", dataType: ATCustomDataType.Boolean },
    { field: "UpdatedOn", displayName: "Updated Date", dataType: ATCustomDataType.Date, isUTCDate : true },
    { field: "CreatedOn", displayName: "Added Date", dataType: ATCustomDataType.Date, isUTCDate : true },
  ];
  //#endregion
}