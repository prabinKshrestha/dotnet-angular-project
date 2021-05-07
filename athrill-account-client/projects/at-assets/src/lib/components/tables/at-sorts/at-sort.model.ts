import { SortDirection } from "at-models";


export interface ATSortItem
{
    field : string;
    displayName : string;
    isSelected : boolean;
    sortDirection : SortDirection;
}