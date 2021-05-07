import { ChangeDetectorRef, Injector } from "@angular/core";
import { forkJoin, Observable, Subscription } from "rxjs";
import { ATBusinessExceptionModel, ODataQueryParameters, SortDirection } from "at-models";
import { ATDetailDialogService, ATDialogService, ATGridDataSource, ATGridState, ATSnackBarService, ATSortItem, ATGridSorts, ATFilterItem, ATGridService, ATUserFilterItem, ATGridFilters } from "at-assets";
import { CommonHelperService } from "at-services";
import * as _ from 'lodash';
/**
 * This abstract class is to be used for grid using component
 *
 * **TODO:** Expand this class from the component and implement the abstracted class
 */
export abstract class ATGridBaseComponent<TModel>{

    abstract displayedColumns: any[] = [];
    abstract sortItems: ATSortItem[] = [];
    abstract filterItems: ATFilterItem[] = [];
    abstract dataFetchAPICall(params?: ODataQueryParameters): Observable<TModel[]>;
    abstract dataTotalCountAPICall(params?: ODataQueryParameters): Observable<number>;
    abstract deleteAPICall(): Observable<boolean>;

    public isContentLoading: boolean = false;

    public gridDataSource: ATGridDataSource<TModel>;
    public gridState: ATGridState;

    protected selectedItem: TModel;
    protected selectedItems: TModel[] = [];

    private _oDataQueryParameters: ODataQueryParameters = new ODataQueryParameters;
    private _subscription: Subscription = new Subscription;

    protected atDialogService: ATDialogService;
    protected atDetailDialogService: ATDetailDialogService;
    protected atSnackBarService: ATSnackBarService;
    protected commonHelperService: CommonHelperService;
    protected atGridService: ATGridService;

    userFilterItems: ATUserFilterItem[] = [];

    public cdf: ChangeDetectorRef;

    constructor(public injector: Injector) {
        this.atDialogService = injector.get(ATDialogService);
        this.atDetailDialogService = injector.get(ATDetailDialogService);
        this.atSnackBarService = injector.get(ATSnackBarService);
        this.commonHelperService = injector.get(CommonHelperService);
        this.atGridService = injector.get(ATGridService);
        this.cdf = injector.get(ChangeDetectorRef);
    }

    //#region LieCycle

    ngOnInit(): void {
        this.restartGrid();
    }

    ngAfterViewChecked(): void {
    }

    ngOnDestroy() {
        this._subscription.unsubscribe();
    }

    //#endregion

    //#region Selections

    /**
     * This method is used for selection
     *
     * **Note:** This method should be overridden by derived class for custom
     */
    onSelection(data: TModel[]) {
        this.selectedItem = data[0];
        this.selectedItems = data;
    }

    //#endregion

    //#region  Grid

    /**
     * This method is used to set the default Grid State for the ATGrid
     *
     * **Note:** This method should be overridden by derived class for custom grid state
     */
    private setDefaultGridState(): ATGridState {
        return this.defaultGridState;
    } 

    /**
     * This method is used to set the default Grid State for the ATGrid
     *
     * **Note:** This method should be overridden by derived class for custom grid state
     */
    public get defaultGridState(): ATGridState{
        let gridState = new ATGridState;
        gridState.pageSize = 10;
        gridState.pageNumber = 1;
        gridState.totalCount = 0;
        gridState.sorts = this.sortItems.filter(x => x.isSelected).map(x => new ATGridSorts(x.field,x.displayName,x.sortDirection));
        gridState.filters = this.userFilterItems.map(x => new ATGridFilters(x.field, x.comparisionType, x.value, x.filterInfo));
        return gridState;
    }

    /**
     * This method is called when any state of the grid changes. Basically, grid state event emitted is catched by this method
     *
     * **Note:** This method should be overridden by derived class for custom changes in grid state
     */
    onGridStateChange(state: ATGridState) {
        this.gridState = state;
        this.getRecordsFromServer();
    }

    //#endregion

    //#region Records

    /**
     * This method resets all the gird states and calls the api to fetch records as initial state.
     *
     * **Note:** This method should be overridden by derived class for custom changes in grid state
     */
    protected restartGrid() {
        this.gridState = this.setDefaultGridState();
        this.getRecordsFromServer();
    }

    /**
     * This method resets the grid state while preserving some states and calls the api to fetch records with set odata parameters.
     *
     * **Note:** This method should be overridden by derived class for custom changes in grid state
     */
    protected restartGridWithPreserveState() {
        this.gridState =  _.clone(this.setDefaultGridStateWithPreserveState());
        this.getRecordsFromServer();
    }

    /**
     * This method is used to set the Grid State  preserving some states for the ATGrid
     *
     * **Note:** This method should be overridden by derived class for custom grid state
     */
    protected setDefaultGridStateWithPreserveState(): ATGridState {
        let gridState: ATGridState = this.gridState;
        // gridState.pageSize = 10; //preserve pageSize
        gridState.pageNumber = 1;
        gridState.totalCount = 0;
        // gridState.sorts = 10; //preserve sorts
        // gridState.filters = 10; //preserve filters
        return gridState;
    }

    /**
     * This method is called when any state of the grid changes. Basically, grid state event emitted is catched by this method
     *
     * **Note:** This method should be overridden by derived class for custom changes in grid state
     */
    protected getRecordsFromServer() {
        this.isContentLoading = true;
        this._oDataQueryParameters = this._setODataQueryParameters();
        //TODO : set different odata params for total count api call ASAP
        let request: Subscription = forkJoin([this.dataFetchAPICall(this._oDataQueryParameters), this.dataTotalCountAPICall(this._oDataQueryParameters)]).subscribe({
            next: (res) => {
                this.gridDataSource = {
                    data: res[0],
                    totalCount: res[1]
                };
                this.selectedItem = null;
            },
            error: (error) => {
                this.atDialogService.openAlertDialog("Error while fetching records. Please try again. If problem persist, then contact administator.");
            },
            complete: () => {
                this.isContentLoading = false;
            }
        });
        this._subscription.add(request);
    }

    //#endregion

    //#region ODataQueryParameters

    private _setODataQueryParameters(): ODataQueryParameters {
        let params: ODataQueryParameters = new ODataQueryParameters;
        params.Expand = this.setExpandODataParams();
        params.Top = this.setTopODataParams();
        params.Skip = this.setSkipODataParams();
        params.Orderby = this.setOrderByODataParams();
        params.Filter = this.setFilterODataParams();
        return params;
    }

    /**
     * This returns the Expand Query Parameters
     *
     * **Note:** This method should be overridden by derived class to set Expand OData Query Parameters
     */
    protected setExpandODataParams(): string {
        return "";
    }

    /**
     * This returns the Top Query Parameters
     *
     * **Note:** This method should be overridden by derived class to set Top OData Query Parameters
     */
    protected setTopODataParams(): number {
        return this.gridState.pageSize;
    }

    /**
     * This returns the Skip Query Parameters
     *
     * **Note:** This method should be overridden by derived class to set Skip OData Query Parameters
     */
    protected setSkipODataParams(): number {
        return (this.gridState.pageNumber - 1) * this.gridState.pageSize;
    }

    /**
     * This returns the Order By Query Parameters
     *
     * **Note:** This method should be overridden by derived class to set Order OData Query Parameters
     */
    protected setOrderByODataParams(): string {
        return this.atGridService.createSortODataParams(this.gridState.sorts);
    }

    /**
     * This returns the Filter Query Parameters
     *
     * **Note:** This method should be overridden by derived class to set Filter OData Query Parameters
     */
    protected setFilterODataParams(): string {
        let retVal : string = this.atGridService.createFilterODataParams(this.gridState.filters);
        return retVal ? retVal : "";
    }

    //#endregion

    //#region Delete

    /**
     * This method is used to delete the item.
     *
     * **Note:** This method should be overridden by derived class for custom changes
     * **TODO:** Should override and implement deleteAPICall abstract method for delete api call. If more usages, then break into different parts
     */
    protected deleteItem(entityName: string = null): void {
        this.atDialogService.openConfirmationDialog("Are you sure you want to delete the item ? This cannot be undone.").subscribe(x => {
            if (x) {
                this.isContentLoading = true;
                this.deleteAPICall().subscribe({
                    next: () => {
                        this.atSnackBarService.show(entityName ? `${entityName} Deleted Successfully` : "Deleted Successfully");
                        this.isContentLoading = false;
                        this.restartGridWithPreserveState();
                    },
                    error: (error: ATBusinessExceptionModel) => {
                        this.atDialogService.openAlertDialog(this.parseError(error));
                        this.isContentLoading = false;
                    }
                });
            }
        });
    }

    //#endregion

    // #region Helpers

    protected parseError(error: ATBusinessExceptionModel): string {
        return this.commonHelperService.parseError(error);
    }

    // #endregion
}