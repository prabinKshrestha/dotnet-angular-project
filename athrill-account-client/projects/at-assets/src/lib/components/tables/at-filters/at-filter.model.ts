import { ATCustomDataType, ComparisionTypes } from "at-models";
import { Observable } from "rxjs";


export interface DropDownOption {
    dataFetchFn: Observable<any[]>;
    displayFn: Function;
    dataValueFn: Function;
    createFilterStringFn?: Function;
    isValueString?: boolean;
}

export interface ATFilterItem {
    field: string;
    displayName: string;
    dataType: ATCustomDataType;
    value?: any;
    dropDownOption?: DropDownOption;
    isUTCDate?: boolean;
}

export class ATUserFilterInfo {

    constructor(dataType: ATCustomDataType, isDropdownValueString : boolean, createFilterStringFn : Function){
        this.dataType = dataType;
        this.isDropdownValueString = isDropdownValueString;
        this.createFilterStringFn = createFilterStringFn;
    }
    dataType: ATCustomDataType;
    isDropdownValueString?: boolean;
    createFilterStringFn?: Function;
    isUTCDate?: boolean;
}

export class ATUserFilterItem {
    field: string;
    comparisionType: ComparisionTypes;
    value?: any;
    filterInfo?: ATUserFilterInfo;
}
