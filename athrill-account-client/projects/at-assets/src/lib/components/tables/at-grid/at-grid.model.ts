import {  ComparisionTypes, SortDirection } from "at-models";
import {  ATUserFilterInfo } from "../at-filters/at-filter.model";


export interface ATGridDataSource<T>{
    totalCount : number;
    data : T[];
}

export class ATGridState
{
    pageNumber: number;
    pageSize : number;
    totalCount : number;
    sorts : ATGridSorts[];
    filters : ATGridFilters[];
}

export class ATGridSorts
{
    constructor(field: string,displayName: string,sortDirection: SortDirection){
        this.field = field;
        this.displayName = displayName;
        this.sortDirection = sortDirection;
    }

    field : string;
    displayName : string;
    sortDirection : SortDirection;
}

export class ATGridFilters
{
    constructor(field: string, comparisionType : ComparisionTypes, value: any, filterInfo: ATUserFilterInfo){
        this.field = field;
        this.comparisionType = comparisionType;
        this.value = value;
        this.filterInfo = filterInfo;
    }

    field : string;
    comparisionType : ComparisionTypes;
    value : any;
    filterInfo : ATUserFilterInfo;
}