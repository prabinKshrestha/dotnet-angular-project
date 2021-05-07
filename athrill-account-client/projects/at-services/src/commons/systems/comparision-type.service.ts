import { Injectable } from '@angular/core';
import {
    ComparisionTypes,
    ATCustomDataType,
    ComparisionTypeModel,
    TotalComparisionTypeCollection,
    BOOLEAN_COMPARISION_TYPE_COLLECTION,
    INTEGER_COMPARISION_TYPE_COLLECTION,
    STRING_COMPARISION_TYPE_COLLECTION,
    DATE_COMPARISION_TYPE_COLLECTION,
    DROPDOWN_COMPARISION_TYPE_COLLECTION
} from 'at-models';

@Injectable({
    providedIn: 'root'
})

export class ComparisionTypeService {

    constructor() { }

    getComparisionsByDataTypeId(dataType: ATCustomDataType): ComparisionTypeModel[] {
        let retVal: ComparisionTypeModel[] = [];
        switch (dataType) {
            case ATCustomDataType.Boolean:
                retVal = this._getComparisionsByComparisionTypeId(BOOLEAN_COMPARISION_TYPE_COLLECTION);
                break;
            case ATCustomDataType.Integer:
                retVal = this._getComparisionsByComparisionTypeId(INTEGER_COMPARISION_TYPE_COLLECTION);
                break;
            case ATCustomDataType.String:
                retVal = this._getComparisionsByComparisionTypeId(STRING_COMPARISION_TYPE_COLLECTION);
                break;
            case ATCustomDataType.Date:
                retVal = this._getComparisionsByComparisionTypeId(DATE_COMPARISION_TYPE_COLLECTION);
            break;
            case ATCustomDataType.Dropdown:
                retVal = this._getComparisionsByComparisionTypeId(DROPDOWN_COMPARISION_TYPE_COLLECTION);
                break;
            default:
                throw Error("Custom Data Type Not Found.");
        }
        return retVal;
    }

    private _getComparisionsByComparisionTypeId(comparisionTypes: ComparisionTypes[]): ComparisionTypeModel[] {
        return TotalComparisionTypeCollection.filter(x => comparisionTypes.indexOf(x.ComparisionTypeId) > -1);
    }

}
