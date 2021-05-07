import { Component, Injector, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ATCustomDataType, ContentModel, ContentTypeModel, ODataQueryParameters, SortDirection } from 'at-models';
import { CommonHelperService, ContentService } from 'at-services';
import { ATMenuItem, ATDetailDialogService, ATAddEditDialogService, ATSortItem, ATFilterItem } from 'at-assets';

import { ATGridBaseComponent } from '../../../../shared/components/grid/at-grid-base.component';
import { ContentFormComponent } from './elements/content-form/content-form.component';
import { ContentDetailComponent } from './elements/content-detail/content-detail.component';

@Component({
  selector: 'at-content-general',
  templateUrl: './content-general.component.html'
})
export class ContentGeneralComponent extends ATGridBaseComponent<ContentModel> implements OnInit {

  moreActions: ATMenuItem[] = [];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _atDetailDialogService: ATDetailDialogService,
    private _atAddEditDialogService: ATAddEditDialogService,
    private _contentService: ContentService,
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
        onClick: () => this.deleteItem("Content")
      }
    ];
    this._atAddEditDialogService.dialogClosed.subscribe(res => {
      if (res && res.isContentAddEdit) {
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
    this._atAddEditDialogService.openAddEditDialog(ContentFormComponent, this._commonHelperService.clone(this.selectedItem));
  }

  onAddItem() {
    this._atAddEditDialogService.openAddEditDialog(ContentFormComponent, new ContentModel);
  }

  onDoubleClick(item: ContentModel) {
    this._atDetailDialogService.openDetailDialog(ContentDetailComponent, item);
  }

  filterParentContentsDataFetchFn(): Observable<ContentModel[]> {
    //TODO: Optimize this ASAP
    let params : ODataQueryParameters = new ODataQueryParameters;
    params.Orderby = "Name ASC";
    return this._contentService.getContents(params);
  }
  filterParentContentsDisplayWithFn(value: ContentModel) {
    return value?.Name;
  }
  filterParentContentsValueFn(value: ContentModel) {
    return value?.Id;
  }


  filterContentTypesDataFetchFn(): Observable<ContentTypeModel[]> {
    return this._contentService.getAllContentTypes();
  }
  filterContentTypesDisplayWithFn(value: ContentTypeModel) {
    return value?.DisplayName;
  }
  filterContentTypesValueFn(value: ContentTypeModel) {
    return value?.Id;
  }

  //#region Overrides

  dataFetchAPICall(params?: ODataQueryParameters): Observable<ContentModel[]> {
    return this._contentService.getContents(params);
  }

  dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number> {
    return this._contentService.getCounts(params);
  }

  setExpandODataParams(): string {
    return "ContentType,Parent,Placement ";
  }

  deleteAPICall(): Observable<boolean> {
    return this._contentService.deleteContent(this.selectedItem.Id);
  }

  displayedColumns: any[] = ["Image", "Name", "ContentType", "Parent", "Slug", "IsPublished"];

  sortItems: ATSortItem[] = [
    { field: "CreatedOn", displayName: "Added Date", isSelected: true, sortDirection: SortDirection.Descending, },
    { field: "UpdatedOn", displayName: "Update Date", isSelected: false, sortDirection: SortDirection.Descending, },
    { field: "IsPublished", displayName: "Publish Status", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Slug", displayName: "Slug", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Position", displayName: "Position", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "Parent.Name", displayName: "Parent", isSelected: false, sortDirection: SortDirection.Ascending, },
    { field: "ContentType.DisplayName", displayName: "Content Type", isSelected: false, sortDirection: SortDirection.Ascending, },
  ];

  filterItems: ATFilterItem[] = [
    { field: "Name", displayName: "Name", dataType: ATCustomDataType.String },
    { field: "Slug", displayName: "Slug", dataType: ATCustomDataType.String },
    {
      field: "ContentTypeId",
      displayName: "Content Type",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterContentTypesDataFetchFn(),
        displayFn: this.filterContentTypesDisplayWithFn.bind(this),
        dataValueFn: this.filterContentTypesValueFn.bind(this)
      }
    },
    {
      field: "ParentId",
      displayName: "Parent",
      dataType: ATCustomDataType.Dropdown,
      dropDownOption: {
        dataFetchFn: this.filterParentContentsDataFetchFn(),
        displayFn: this.filterParentContentsDisplayWithFn.bind(this),
        dataValueFn: this.filterParentContentsValueFn.bind(this)
      }
    },
    { field: "IsPublished", displayName: "Publish Status", dataType: ATCustomDataType.Boolean },
    { field: "UpdatedOn", displayName: "Updated Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
    { field: "CreatedOn", displayName: "Added Date", dataType: ATCustomDataType.Date, isUTCDate : true  },
  ];

  //#endregion
}