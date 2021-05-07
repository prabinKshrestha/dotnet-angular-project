import { Injectable } from '@angular/core';
import { ATCustomDataType, ComparisionTypes } from 'at-models';
import { ATUserFilterItem } from './at-filter.model';

@Injectable({
    providedIn: 'root'
})

export class ATFilterService {

    constructor() { }

    isUserFilterItemValid(userFilterItem : ATUserFilterItem) : boolean{
        let retVal : boolean = true;
        switch(userFilterItem.filterInfo?.dataType){
            case ATCustomDataType.Boolean:
                break;
            case ATCustomDataType.Integer:
                retVal = this._validateInteger(userFilterItem.value);
                break;
            case ATCustomDataType.String:
                break;
            case ATCustomDataType.Date:
                retVal = this._validateDate(userFilterItem);
                break;
            case ATCustomDataType.Dropdown:
                break;
            default:
                retVal = false;
                break;
        }
        return retVal;
    }

    modifyUserFilterItem(userFilterItem : ATUserFilterItem) : ATUserFilterItem{
        switch(userFilterItem.filterInfo?.dataType){
            case ATCustomDataType.Boolean:
                userFilterItem.value = this._modifyBoolean(userFilterItem.value);
                break;
            case ATCustomDataType.String:
                userFilterItem.value = this._modifyString(userFilterItem);
                break;
            case ATCustomDataType.Date:
                userFilterItem.value = this._modifyDate(userFilterItem);
                break;
            case ATCustomDataType.Dropdown:
                userFilterItem.value = this._modifyDropdown(userFilterItem);
                break;
        }
        return userFilterItem;
    }

    private _modifyBoolean(value : any): boolean{
        return !!value;
    }

    private _validateInteger(value : any): boolean{
        return value;
    }

    private _validateDate(userFilterItem : ATUserFilterItem): boolean{
       if(!(userFilterItem.comparisionType == ComparisionTypes.IsNotNull || userFilterItem.comparisionType == ComparisionTypes.IsNull) && !userFilterItem.value){
           return false;
       }
        return true;
    }

    private _modifyString(userFilterItem : ATUserFilterItem): string{
        if(userFilterItem.comparisionType == ComparisionTypes.IsNull || userFilterItem.comparisionType == ComparisionTypes.IsNotNull){
            return null;
        }else{
            return userFilterItem.value ? userFilterItem.value : '';
        }
    }

    private _modifyDate(userFilterItem : ATUserFilterItem): any{
        return userFilterItem.value;
    }

    private _modifyDropdown(userFilterItem : ATUserFilterItem): any{
        if(userFilterItem.value === undefined){
            return null;
        }
        return userFilterItem.value;
    }

}