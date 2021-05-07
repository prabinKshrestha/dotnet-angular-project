import { Component, OnInit, Injector } from '@angular/core';
import { Observable } from 'rxjs';

import { ATCustomDataType, ComparisionTypes, ODataQueryParameters, SortDirection, UserModel, UserTrackRecordModel } from 'at-models';
import { UserContextService, UserService } from 'at-services';
import { ATFilterItem, ATGridState, ATMenuItem, ATSortItem, ATUserFilterItem, ATGridFilters, ATUserFilterInfo } from 'at-assets';
import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';

@Component({
  selector: 'at-profile-auth-track',
  templateUrl: './profile-auth-track.component.html'
})
export class ProfileAuthTrackComponent extends ATGridBaseComponent<UserTrackRecordModel> implements OnInit {

  moreActions: ATMenuItem[] = [];
  users: UserModel[] = [];

  userTrackTypes = [
    { Id: 0, DisplayName: "All" },
    { Id: 1, DisplayName: "Login" },
    { Id: 2, DisplayName: "Log Out" },
    { Id: 3, DisplayName: "Register User" },
    { Id: 4, DisplayName: "Activate" },
    { Id: 5, DisplayName: "Deactivate" },
    { Id: 6, DisplayName: "Change Password" },
    { Id: 7, DisplayName: "Reset Password" },
  ];

  constructor(
    private _userService: UserService,
    private _userContextService: UserContextService,
    public injector: Injector
  ) {
    super(injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
    this._getPreDatas();
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
    this.cdf.detectChanges();
  }

  getCreatedByFullName(createdById: number) {
    return this.users.find(x => x.Id == createdById)?.FullName;
  }

  onUserTrackTypeChange(id: number) {
    this.restartGridWithPreserveState();
  }

  private _getPreDatas(): void {
    this.isContentLoading = true;
    this._userService.getUsers().subscribe(users => {
      this.users = users;
      this.isContentLoading = false;
    });
  }


  filterUserTrackTypesDataFetchFn(): Observable<any[]> {
    return new Observable((observer) => {
      observer.next(this.userTrackTypes);
    });
  }
  filterUserTrackTypesDisplayWithFn(value: any) {
    return value?.DisplayName;
  }
  filterUserTrackTypesValueFn(value: any) {
    return value?.Id;
  }
  filterUserTrackTypesCreateFilterFn(filter: ATGridFilters) {
    switch (filter.comparisionType) {
      case ComparisionTypes.IsEqualTo:
        return filter.value ? `UserTrackTypeId EQ ${filter.value}` : ``;
      case ComparisionTypes.IsNotEqualTo:
        return filter.value ? `UserTrackTypeId NEQ ${filter.value}` : ``;
      case ComparisionTypes.IsNull:
        return `UserTrackTypeId EQ NULL`;
      case ComparisionTypes.IsNotNull:
        return `UserTrackTypeId NEQ NULL`;
    }
  }

  //#region Overrides

  dataFetchAPICall(params?: ODataQueryParameters): Observable<UserTrackRecordModel[]> {
    return this._userService.getUserTrackRecords(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._userService.getCountsForUserTrackRecords(params);
  }

  deleteAPICall(): Observable<boolean> {
    throw new Error('Method not implemented.');
  }

  setFilterODataParams(): string {
    let retVal: string = super.setFilterODataParams();
    retVal += `${retVal.trim() == "" ? `` : `AND`} UserId == ${this._userContextService.get()?.UserId}`;
    return retVal;
  }

  displayedColumns: any[] = ["UserTrackTypeDisplayName", "IpAddress", "ClientName", "PerformedBy", "CreatedOn"];

  sortItems: ATSortItem[] = [
    { field: "CreatedOn", displayName: "Added Date", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "IpAddress", displayName: "Ip Address", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "ClientName", displayName: "Client Name", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [
    {
      field: "UserTrackTypeId", // will not be used
      displayName: "Action",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterUserTrackTypesDataFetchFn(),
        displayFn: this.filterUserTrackTypesDisplayWithFn.bind(this),
        dataValueFn: this.filterUserTrackTypesValueFn.bind(this),
        createFilterStringFn: this.filterUserTrackTypesCreateFilterFn.bind(this)
      }
    },
    { field: "IpAddress", displayName: "Ip Address", dataType: ATCustomDataType.String },
    { field: "ClientName", displayName: "Client Info", dataType: ATCustomDataType.String },
    { field: "CreatedOn", displayName: "Added Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
  ];

  userFilterItems: ATUserFilterItem[] = [
    {
      field: "UserTrackTypeId",
      comparisionType: ComparisionTypes.IsEqualTo,
      value: 0,
      filterInfo: {
        createFilterStringFn: this.filterUserTrackTypesCreateFilterFn.bind(this),
        dataType: ATCustomDataType.Dropdown,
        isDropdownValueString: false
      }
    }
  ];

  public get defaultGridState(): ATGridState {
    let gridState = super.defaultGridState;
    gridState.pageSize = 20;
    return gridState;
  }

  //#endregion
}