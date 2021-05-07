import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ATCustomDataType, ODataQueryParameters, SortDirection, TeamCategoryModel } from 'at-models';
import { CommonHelperService, TeamCategoryService } from 'at-services';
import { ATAddEditDialogService, ATDetailDialogService, ATFilterItem, ATMenuItem, ATSortItem } from 'at-assets';

import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';
import { TeamCategoryDetailComponent } from './elements/team-category-detail/team-category-detail.component';
import { TeamCategoryFormComponent } from './elements/team-category-form/team-category-form.component';

@Component({
  selector: 'at-team-category',
  templateUrl: './team-category.component.html'
})
export class TeamCategoryComponent extends ATGridBaseComponent<TeamCategoryModel> implements OnInit {

  moreActions: ATMenuItem[] = [];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _atDetailDialogService: ATDetailDialogService,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _teamCategoryService: TeamCategoryService,
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
        onClick: () => this.deleteItem("Team Category")
      }
    ];
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isTeamCategoryAddEdit) {
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
    this._atAddEditDialogService.openAddEditDialog(TeamCategoryFormComponent, this._commonHelperService.clone(this.selectedItem));
  }

  onAddItem() {
    this._atAddEditDialogService.openAddEditDialog(TeamCategoryFormComponent, new TeamCategoryModel);
  }

  onDoubleClick(item: TeamCategoryModel) {
    this._atDetailDialogService.openDetailDialog(TeamCategoryDetailComponent, item);
  }

  //#region Overrides

  dataFetchAPICall(params?: ODataQueryParameters): Observable<TeamCategoryModel[]> {
    return this._teamCategoryService.getTeamCategories(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._teamCategoryService.getCounts(params);
  }

  deleteAPICall(): Observable<boolean> {
    return this._teamCategoryService.deleteTeamCategory(this.selectedItem.Id);
  }

  displayedColumns: any[] = ["Name", "ShortDescription", "IsPublished"];

  sortItems: ATSortItem[] = [
    { field: "CreatedOn", displayName: "Added Date", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "UpdatedOn", displayName: "Update Date", isSelected: false, sortDirection: SortDirection.Descending, },
    { field: "Name", displayName: "Name", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Orientation", displayName: "Orientation", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "IsPublished", displayName: "Publish Status", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [
    { field: "Name", displayName: "Name", dataType: ATCustomDataType.String },
    { field: "ShortDescription", displayName: "ShortDescription", dataType: ATCustomDataType.String },
    { field: "IsPublished", displayName: "Publish Status", dataType: ATCustomDataType.Boolean },
    { field: "UpdatedOn", displayName: "Updated Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
    { field: "CreatedOn", displayName: "Added Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
  ];

  //#endregion
}