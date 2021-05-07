import { Component, EventEmitter, OnInit, ViewChild, Output, Input } from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { ComparisionTypeModel, ATCustomDataType, ComparisionTypes } from 'at-models';
import { ComparisionTypeService } from 'at-services';
import { ATFilterItem, ATUserFilterInfo, ATUserFilterItem, DropDownOption } from './at-filter.model';
import { ATFilterService } from './at-filter.service';
import * as _ from 'lodash';

@Component({
    selector: 'at-assets-filter',
    templateUrl: './at-filter.component.html',
    styleUrls: ['./at-filter.component.scss']
})
export class ATFilterComponent implements OnInit {

    @ViewChild(MatMenu, { static: true }) matMenu: MatMenu;
    @ViewChild('matMenuTrigger') matMenuTrigger: MatMenuTrigger;

    @Input() filterItems: ATFilterItem[] = [];
    @Input() set userFilterItems(value: ATUserFilterItem[]) {
        this.currentUserFilterItems = _.cloneDeep(value.length == 0 ? [this._emptyUserFilterItem] : value);
        this._originalFilterItems = _.cloneDeep(this.currentUserFilterItems);
    }

    @Output() onSave: EventEmitter<ATUserFilterItem[]> = new EventEmitter();
    @Output() onClear: EventEmitter<void> = new EventEmitter();

    private _emptyUserFilterItem: ATUserFilterItem = { field: null, value: null, comparisionType: 0 };
    currentUserFilterItems: ATUserFilterItem[] = [this._emptyUserFilterItem];
    private _originalFilterItems: ATUserFilterItem[] = [];

    private _dropDownCache: Array<{ field: string, data: any[] }> = [];

    showFilterLoading: boolean = false;
    ATCustomDataTypeEnum = ATCustomDataType;

    constructor(
        private _comparisionTypeService: ComparisionTypeService,
        private _atFilterService: ATFilterService
    ) { }

    ngOnInit(): void {
    }

    getComparisionTypes(userFilterItem: ATUserFilterItem): ComparisionTypeModel[] {
        if (userFilterItem.field) {
            return this._comparisionTypeService.getComparisionsByDataTypeId(this.filterItems.find(x => x.field == userFilterItem.field)?.dataType);
        }
    }

    getDataType(userFilterItem: ATUserFilterItem): ATCustomDataType {
        let filterItem: ATFilterItem = this.filterItems.find(x => x.field == userFilterItem.field);
        return filterItem && userFilterItem.comparisionType ? filterItem.dataType : null;
    }

    getDropDownOption(userFilterItem: ATUserFilterItem): DropDownOption {
        return this.filterItems.find(x => x.field == userFilterItem.field).dropDownOption;
    }

    getDataForDropDown(userFilterItem: ATUserFilterItem): any[] {
        if (this._dropDownCache.find(x => x.field == userFilterItem.field)) {
            return this._dropDownCache.find(x => x.field == userFilterItem.field).data;
        } else {
            this.showFilterLoading = true;
            this._dropDownCache.push({ field: userFilterItem.field, data: [] });
            this.filterItems.find(x => x.field == userFilterItem.field).dropDownOption.dataFetchFn.subscribe({
                next: (res) => {
                    this.showFilterLoading = false;
                    this._dropDownCache.find(x => x.field == userFilterItem.field).data = res;
                },
                error: () => {
                    this._dropDownCache.splice(this._dropDownCache.findIndex(x => x.field == userFilterItem.field),0);
                }
            });
        }
    }

    isValueDisabled(userFilterItem: ATUserFilterItem) {
        let retVal = userFilterItem.comparisionType == ComparisionTypes.IsNull || userFilterItem.comparisionType == ComparisionTypes.IsNotNull;
        if (retVal) {
            userFilterItem.value = null;
        }
        return retVal;
    }

    onAddClick() {
        this.currentUserFilterItems.push(_.cloneDeep(this._emptyUserFilterItem));
    }

    onRemoveClick(index: number) {
        this.currentUserFilterItems.splice(index, 1);
        if (this.currentUserFilterItems.length == 0) {
            this.currentUserFilterItems.push(_.cloneDeep(this._emptyUserFilterItem));
        }
    }

    onSaveClick() {
        this.currentUserFilterItems = this.currentUserFilterItems.filter(x => x.field && x.comparisionType).map(x => {
            let info = this.filterItems.find(y => y.field == x.field);
            x.filterInfo = new ATUserFilterInfo(info.dataType, info.dropDownOption?.isValueString, info.dropDownOption?.createFilterStringFn);
            x.filterInfo.isUTCDate = !!info.isUTCDate;
            return x;
        }).filter(x => this._atFilterService.isUserFilterItemValid(x)).map(x => this._atFilterService.modifyUserFilterItem(x));
        this.onSave.emit(this.currentUserFilterItems);
        this._originalFilterItems = _.cloneDeep(this.currentUserFilterItems);
        this.matMenuTrigger.closeMenu();
    }

    onClearClick() {
        this.onClear.emit();
        this.matMenuTrigger.closeMenu();
    }

    onCloseMatMenu(reason: any) {
        this.currentUserFilterItems = _.cloneDeep(this._originalFilterItems);
    }

}
