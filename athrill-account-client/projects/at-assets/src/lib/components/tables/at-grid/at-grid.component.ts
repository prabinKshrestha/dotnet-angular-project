
import { SelectionModel } from '@angular/cdk/collections';
import { AfterContentInit, ChangeDetectorRef, Component, ContentChildren, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, TemplateRef, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatColumnDef, MatTable, MatTableDataSource } from '@angular/material/table';
import { ATMenuItem } from '../../items/at-menus/at-menus.model';
import { ATFilterItem, ATUserFilterItem } from '../at-filters/at-filter.model';
import { ATSortItem } from '../at-sorts/at-sort.model';
import { ATGridDataSource, ATGridFilters, ATGridSorts, ATGridState } from './at-grid.model';
import * as _ from 'lodash';


const PAGE_SIZE_OPITONS = [10, 20, 30, 40, 50];

@Component({
  selector: 'at-assets-grid',
  templateUrl: './at-grid.component.html',
  styleUrls: ['./at-grid.component.scss']
})

export class ATGridComponent<T> implements OnInit, AfterContentInit {

  @ViewChild(MatTable, { static: true }) table: MatTable<T>;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ContentChildren(MatColumnDef) columnDefs: QueryList<MatColumnDef>;

  @Input() displayedColumns: string[] = [];
  @Input() set gridDataSource(value: ATGridDataSource<T>) {
    if (value) {
      this._data = value.data;
      this.totalDataCount = value.totalCount;
      this.dataSource = new MatTableDataSource<T>(this._data);
    }
  }

  @Input() set isMultipleSelectionAllowed(value: boolean) {
    this._selection = new SelectionModel<T>(value, null);
    this._isMultipleSelectionAllowed = value;
  }

  @Input() hasPagination: boolean = true;
  @Input() paginatorColor: 'primary' | 'accent' | 'basic' = 'accent';
  @Input() set gridState(value: ATGridState) {
    this.pageIndex = value.pageNumber - 1;
    this.pageSize = value.pageSize;
    this.totalDataCount = value.totalCount;
    this._currentGridState = _.clone(value);
  }
  @Input() defaultGridState: ATGridState;

  @Input() sortItems: ATSortItem[] = [];
  @Input() filterItems: ATFilterItem[] = [];
  @Input() userFilterItems: ATUserFilterItem[] = [];

  @Output() onSelection: EventEmitter<T[]> = new EventEmitter<T[]>();
  @Output() onDoubleClick: EventEmitter<T> = new EventEmitter<T>();

  @Output() onGridStateChange: EventEmitter<ATGridState> = new EventEmitter<ATGridState>();


  get data() {
    return this._data;
  }
  private _data: T[] = [];

  dataSource: MatTableDataSource<T>;

  totalDataCount: number = 0;
  pageSizeOptions: number[] = PAGE_SIZE_OPITONS;
  pageIndex: number = 0;
  pageSize: number = PAGE_SIZE_OPITONS[0];


  private _currentGridState: ATGridState;

  private _isMultipleSelectionAllowed: boolean = false;
  private _selection: SelectionModel<T> = new SelectionModel<T>(false, null);

  gridMenuItems: ATMenuItem[] = [];

  constructor(private _cdf: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.gridMenuItems = [
      {
        key: "clear_sortings",
        displayName: "Clear Sorts",
        onClick: () => this.onClearSortItems()
      },
      {
        key: "clear_filters",
        displayName: "Clear Filters",
        onClick: () => this.onClearFilterItems()
      },
      {
        key: "clear_pagination",
        displayName: "Clear Pagination",
        onClick: () => this._clearPagination()
      },
      {
        key: "clear_all",
        displayName: "Clear All",
        onClick: () => this._clearAll()
      }
    ];
  }

  ngAfterViewChecked(): void {
    this.gridMenuItems = this.gridMenuItems.map(x => {
      switch (x.key) {
        case "clear_sortings":
          x.isDisabled = this.sortItems.length < 1;
          break;
        case "clear_filters":
          x.isDisabled = this.filterItems.length < 1;
          break;
      }
      return x;
    });
    this._cdf.detectChanges();
  }


  ngAfterContentInit() {
    this.columnDefs.forEach(columnDef => this.table.addColumnDef(columnDef));
  }

  isRowSelected(row: T): boolean {
    return this._selection.isSelected(row);
  }

  onRowSelection(row: T) {
    this._selection.select(row);
    this.onSelection.emit(this._selection.selected);
  }

  onDblClick(row: T) {
    this.onDoubleClick.emit(row);
  }

  private _clearAll() {
    this._clearSort();
    this._clearFilter();
    this.onGridStateChange.emit(this.defaultGridState);
  }

  //#region Pagination

  onPaginatorEvent(pageEvent: PageEvent) {
    this._currentGridState.pageSize = pageEvent.pageSize;
    this._currentGridState.pageNumber = pageEvent.pageIndex + 1;
    this._currentGridState.totalCount = pageEvent.length;
    this.onGridStateChange.emit(this._currentGridState);
  }

  private _clearPagination() {
    this._currentGridState.pageSize = this.defaultGridState.pageSize
    this._currentGridState.pageNumber = this.defaultGridState.pageNumber;
    this._currentGridState.totalCount = this.defaultGridState.totalCount;
    this.onGridStateChange.emit(this._currentGridState);
  }

  //#endregion

  //#region Sorts

  onSaveSortItems(data: ATSortItem[]) {
    this._currentGridState.sorts = data.filter(x => x.isSelected).map(x => new ATGridSorts(x.field, x.displayName, x.sortDirection));
    this.onGridStateChange.emit(this._currentGridState);
  }

  onClearSortItems() {
    this._clearSort();
    this.onGridStateChange.emit(this._currentGridState);
  }

  private _clearSort() {
    this.sortItems = _.clone(this.sortItems);
    this._currentGridState.sorts = this.sortItems.filter(x => x.isSelected).map(x => new ATGridSorts(x.field, x.displayName, x.sortDirection));
  }

  //#endregion

  //#region Filters

  onSaveFilterItems(data: ATUserFilterItem[]) {
    this._currentGridState.filters = data.map(x => new ATGridFilters(x.field, x.comparisionType, x.value, x.filterInfo));
    this._currentGridState.pageNumber = this.defaultGridState.pageNumber;
    this._currentGridState.totalCount = this.defaultGridState.totalCount;
    this.onGridStateChange.emit(this._currentGridState);
  }

  onClearFilterItems() {
    this._clearFilter();
    this._currentGridState.pageNumber = this.defaultGridState.pageNumber;
    this._currentGridState.totalCount = this.defaultGridState.totalCount;
    this.onGridStateChange.emit(this._currentGridState);
  }

  private _clearFilter() {
    this.userFilterItems = _.clone(this.userFilterItems);
    this._currentGridState.filters = this.userFilterItems.map(x => new ATGridFilters(x.field, x.comparisionType, x.value, x.filterInfo));
  }

  //#endregion
}
