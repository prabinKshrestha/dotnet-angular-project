import { Injectable } from '@angular/core';
import { ATCustomDataType, ComparisionTypes, SortDirection } from 'at-models';
import { DateHelperService } from 'at-services';
import { ATGridFilters, ATGridSorts } from './at-grid.model';
import * as moment from 'moment';

@Injectable({
    providedIn: 'root'
})

export class ATGridService {

    constructor(
        private _dateHelperService : DateHelperService
      ) { }
    

    //#region Sorts

    createSortODataParams(sorts : ATGridSorts[]) : string{
        return sorts.map(x => `${x.field} ${x.sortDirection == SortDirection.Ascending ? 'ASC' : 'DESC'}`).join(',');
    }

    //#endregion

    //#region Filters

    createFilterODataParams(filters : ATGridFilters[]) : string{
        if(!filters || filters.length == 0){
            return;
        }
        let retVal : string = '';
        filters.forEach(f => {
            let concatValue : string = this._getComparisionWithValue(f);
            if(concatValue && concatValue.trim() != ""){
                retVal += `${concatValue} AND `;
            }
        });
        return retVal.endsWith("AND ") ? retVal.slice(0,-4): retVal;
    }

    private _getComparisionWithValue(filter : ATGridFilters): string{        
        switch(filter.filterInfo?.dataType){
            case ATCustomDataType.Boolean:
                return this._getFilterForBoolean(filter);
            case ATCustomDataType.Integer:
                return this._getFilterForInteger(filter);
            case ATCustomDataType.String:
                return this._getFilterForString(filter);
            case ATCustomDataType.Date:
                return this._getFilterForDate(filter);
            case ATCustomDataType.Dropdown:
                return this._getFilterForDropdown(filter);
        } 
    }

    private _getFilterForBoolean(filter : ATGridFilters): string{
        switch(filter.comparisionType){
            case ComparisionTypes.IsEqualTo:
                return `${filter.field} EQ ${filter.value}`;
            case ComparisionTypes.IsNotEqualTo:
                return `${filter.field} NEQ ${filter.value}`;
            case ComparisionTypes.IsNull:
                return `${filter.field} EQ NULL`;
            case ComparisionTypes.IsNotNull:
                return `${filter.field} NEQ NULL`;
        }
    }

    private _getFilterForInteger(filter : ATGridFilters): string{
        switch(filter.comparisionType){
            case ComparisionTypes.IsEqualTo:
                return `${filter.field} EQ ${filter.value}`;
            case ComparisionTypes.IsNotEqualTo:
                return `${filter.field} NEQ ${filter.value}`;
            case ComparisionTypes.IsLessThan:
                return `${filter.field} < ${filter.value}`;
            case ComparisionTypes.IsLessThanOrEqualTo:
                return `${filter.field} <= ${filter.value}`;
            case ComparisionTypes.IsGreaterThan:
                return `${filter.field} > ${filter.value}`;
            case ComparisionTypes.IsGreaterThanOrEqualTo:
                return `${filter.field} >= ${filter.value}`;
            case ComparisionTypes.IsNull:
                return `${filter.field} EQ NULL`;
            case ComparisionTypes.IsNotNull:
                return `${filter.field} NEQ NULL`;
        }
    }

    private _getFilterForString(filter : ATGridFilters): string{
        switch(filter.comparisionType){
            case ComparisionTypes.IsEqualTo:
                return `${filter.field} EQ "${filter.value}"`;
            case ComparisionTypes.IsNotEqualTo:
                return `${filter.field} NEQ "${filter.value}"`;
            case ComparisionTypes.IsLessThan:
                return `${filter.field} < "${filter.value}"`;
            case ComparisionTypes.IsLessThanOrEqualTo:
                return `${filter.field} <= "${filter.value}"`;
            case ComparisionTypes.IsGreaterThan:
                return `${filter.field} > "${filter.value}"`;
            case ComparisionTypes.IsGreaterThanOrEqualTo:
                return `${filter.field} >= "${filter.value}"`;
            case ComparisionTypes.Contains:
                return `${filter.field}.contains("${filter.value}")`;
            case ComparisionTypes.NotContains:
                return `not ${filter.field}.contains("${filter.value}")`;
            case ComparisionTypes.IsNull:
                return `${filter.field} EQ NULL`;
            case ComparisionTypes.IsNotNull:
                return `${filter.field} NEQ NULL`;
            case ComparisionTypes.StartsWith:
                return `${filter.field}.startsWith("${filter.value}")`;
            case ComparisionTypes.NotStartsWith:
                return `not ${filter.field}.startsWith("${filter.value}")`;
            case ComparisionTypes.EndsWith:
                return `${filter.field}.endsWith("${filter.value}")`;
            case ComparisionTypes.NotEndsWith:
                return `not ${filter.field}.endsWith("${filter.value}")`;
        }
    }

    private _getFilterForDate(filter : ATGridFilters): string{
        if(filter.filterInfo.isUTCDate){
            switch(filter.comparisionType){
                case ComparisionTypes.IsEqualTo:
                    return `(${filter.field} >= "${this._dateHelperService.convertToUTCDate(filter.value, true)}" AND ${filter.field} < "${this._dateHelperService.convertToUTCDate(moment(filter.value).add(1, 'd').format(), true)}")`;
                case ComparisionTypes.IsNotEqualTo:
                    return `(${filter.field} < "${this._dateHelperService.convertToUTCDate(filter.value, true)}" OR ${filter.field} >= "${this._dateHelperService.convertToUTCDate(moment(filter.value).add(1, 'd').format(), true)}")`;
                case ComparisionTypes.IsLessThan:
                    return `${filter.field} < "${this._dateHelperService.convertToUTCDate(filter.value, true)}"`;
                case ComparisionTypes.IsLessThanOrEqualTo:
                    return `${filter.field} < "${this._dateHelperService.convertToUTCDate(moment(filter.value).add(1, 'd').format(), true)}"`;
                case ComparisionTypes.IsGreaterThan:
                    return `${filter.field} >= "${this._dateHelperService.convertToUTCDate(moment(filter.value).add(1, 'd').format(), true)}"`;
                case ComparisionTypes.IsGreaterThanOrEqualTo:
                    return `${filter.field} >= "${this._dateHelperService.convertToUTCDate(filter.value, true)}"`;
                case ComparisionTypes.IsNull:
                    return `${filter.field} EQ NULL`;
                case ComparisionTypes.IsNotNull:
                    return `${filter.field} NEQ NULL`;
            }
        }else{
            switch(filter.comparisionType){
                case ComparisionTypes.IsEqualTo:
                    return `(${filter.field} >= "${filter.value}" AND ${filter.field} < "${this._dateHelperService.convertToISOStringWithOutZ(moment(filter.value).add(1, 'd').toDate())}")`;
                case ComparisionTypes.IsNotEqualTo:
                    return `(${filter.field} < "${filter.value}" OR ${filter.field} >= "${this._dateHelperService.convertToISOStringWithOutZ(moment(filter.value).add(1, 'd').toDate())}")`;
                case ComparisionTypes.IsLessThan:
                    return `${filter.field} < "${filter.value}"`;
                case ComparisionTypes.IsLessThanOrEqualTo:
                    return `${filter.field} < "${this._dateHelperService.convertToISOStringWithOutZ(moment(filter.value).add(1, 'd').toDate())}"`;
                case ComparisionTypes.IsGreaterThan:
                    return `${filter.field} >= "${this._dateHelperService.convertToISOStringWithOutZ(moment(filter.value).add(1, 'd').toDate())}"`;
                case ComparisionTypes.IsGreaterThanOrEqualTo:
                    return `${filter.field} >= "${filter.value}"`;
                case ComparisionTypes.IsNull:
                    return `${filter.field} EQ NULL`;
                case ComparisionTypes.IsNotNull:
                    return `${filter.field} NEQ NULL`;
            }
        }
    }

    private _getFilterForDropdown(filter : ATGridFilters): string{
        if(filter.filterInfo?.createFilterStringFn){
            return filter.filterInfo.createFilterStringFn(filter);
        }else{
            switch(filter.comparisionType){
                case ComparisionTypes.IsEqualTo:
                    return filter.filterInfo?.isDropdownValueString ? `${filter.field} EQ "${filter.value}"` : `${filter.field} EQ ${filter.value}` ;
                case ComparisionTypes.IsNotEqualTo:
                    return filter.filterInfo?.isDropdownValueString ? `${filter.field} NEQ "${filter.value}"` : `${filter.field} NEQ ${filter.value}` ;
                case ComparisionTypes.IsNull:
                    return `${filter.field} EQ NULL`;
                case ComparisionTypes.IsNotNull:
                    return `${filter.field} NEQ NULL`;
            }
        }
    }
    //#endregion
}