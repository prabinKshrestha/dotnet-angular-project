import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ATCustomDataType, ComparisionTypes, ODataQueryParameters, SortDirection, TeamCategoryModel, TeamMemberFormModel, TeamMemberModel } from 'at-models';
import { CommonHelperService, TeamCategoryService, TeamMemberService } from 'at-services';
import { ATAddEditDialogService, ATDetailDialogService, ATFilterItem, ATGridFilters, ATMenuItem, ATSortItem } from 'at-assets';

import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';
import { TeamMemberFormComponent } from './elements/team-member-form/team-member-form.component';
import { TeamMemberDetailComponent } from './elements/team-member-detail/team-member-detail.component';

@Component({
  selector: 'at-team-member',
  templateUrl: './team-member.component.html'
})
export class TeamMemberComponent extends ATGridBaseComponent<TeamMemberModel> implements OnInit {

  moreActions: ATMenuItem[] = [];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _atDetailDialogService: ATDetailDialogService,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _teamCategoryService: TeamCategoryService,
    private _teamMemberService: TeamMemberService,
    public injector: Injector
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
        onClick: () => this.deleteItem("Team Member")
      }
    ];
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isTeamMemberAddEdit) {
        this.restartGridWithPreserveState();
      }
    })
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
    this.moreActions = this.moreActions.map(x => {
      switch (x.key) {
        case "edit":
        case "delete":
          x.isDisabled = !this.selectedItem;
      }
      return x;
    });
    this.cdf.detectChanges();
  }

  private _onEditItem() {
    this._atAddEditDialogService.openAddEditDialog(TeamMemberFormComponent, this._commonHelperService.clone(this.selectedItem));
  }

  onAddItem() {
    this._atAddEditDialogService.openAddEditDialog(TeamMemberFormComponent, new TeamMemberFormModel);
  }

  onDoubleClick(item: TeamCategoryModel) {
    this._atDetailDialogService.openDetailDialog(TeamMemberDetailComponent, item);
  }


  filterTeamCategoriesDataFetchFn(): Observable<TeamCategoryModel[]> {
    return this._teamCategoryService.getTeamCategories({ Orderby : "Name ASC" });
  }
  filterTeamCategoriesDisplayWithFn(value: TeamCategoryModel) {
    return value?.Name;
  }
  filterTeamCategoriesValueFn(value: TeamCategoryModel) {
    return value?.Id;
  }
  filterTeamCategoriesCreateFilterFn(filter: ATGridFilters) {
    switch (filter.comparisionType) {
      case ComparisionTypes.IsEqualTo:
        return `TeamCategoryMemberLinks.Any(TeamCategoryId EQ ${filter.value})`;
      case ComparisionTypes.IsNotEqualTo:
        return `NOT TeamCategoryMemberLinks.Any(TeamCategoryId EQ ${filter.value})`;
      case ComparisionTypes.IsNull:
        return `NOT TeamCategoryMemberLinks.Any()`;
      case ComparisionTypes.IsNotNull:
        return `TeamCategoryMemberLinks.Any()`;
    }
  }


  //#region Overrides

  dataFetchAPICall(params?: ODataQueryParameters): Observable<TeamMemberModel[]> {
    return this._teamMemberService.getTeamMembers(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._teamMemberService.getCounts(params);
  }

  deleteAPICall(): Observable<boolean> {
    return this._teamMemberService.deleteTeamMember(this.selectedItem.Id);
  }

  setExpandODataParams(): string {
    return "TeamCategoryMemberLinks.TeamCategory";
  }

  displayedColumns: any[] = ["Image", "Name", "Email", "PhoneNumber", "Position", "IsPublished"];

  sortItems: ATSortItem[] = [
    { field: "CreatedOn", displayName: "Added Date", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "UpdatedOn", displayName: "Update Date", isSelected: false, sortDirection: SortDirection.Descending, },
    { field: "Name", displayName: "Name", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Email", displayName: "Email", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Position", displayName: "Position", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "IsPublished", displayName: "Publish Status", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [
    { field: "Name", displayName: "Name", dataType: ATCustomDataType.String },
    { field: "Email", displayName: "Email", dataType: ATCustomDataType.String },
    { field: "Position", displayName: "Position", dataType: ATCustomDataType.String },
    { field: "IsPublished", displayName: "Publish Status", dataType: ATCustomDataType.Boolean },
    {
      field: "TeamCategoryId", // will not be used
      displayName: "Team Category",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterTeamCategoriesDataFetchFn(),
        displayFn: this.filterTeamCategoriesDisplayWithFn.bind(this),
        dataValueFn: this.filterTeamCategoriesValueFn.bind(this),
        createFilterStringFn: this.filterTeamCategoriesCreateFilterFn.bind(this)
      }
    },
    { field: "UpdatedOn", displayName: "Updated Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
    { field: "CreatedOn", displayName: "Added Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
  ];

  //#endregion
}