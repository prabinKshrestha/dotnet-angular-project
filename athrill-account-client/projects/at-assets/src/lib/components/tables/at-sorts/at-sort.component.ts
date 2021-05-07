import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, Input, OnInit, Output, ViewChild, EventEmitter } from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import * as _ from 'lodash';
import { SortDirection } from 'at-models';
import { ATSortItem } from './at-sort.model';

@Component({
    selector: 'at-assets-sort',
    templateUrl: './at-sort.component.html',
    styleUrls: ['./at-sort.component.scss']
})
export class ATSortComponent implements OnInit {

    @ViewChild(MatMenu, { static: true }) matMenu: MatMenu;
    @ViewChild('matMenuTrigger') matMenuTrigger: MatMenuTrigger;

    @Input() set items (value : ATSortItem[]){
        this.sortItems = _.cloneDeep(value);
        this._originalSortItems = _.cloneDeep(value);
    }

    @Output() onSave : EventEmitter<ATSortItem[]> = new EventEmitter();
    @Output() onClear : EventEmitter<void> = new EventEmitter();

    SortDirections  = SortDirection;

    sortItems : ATSortItem[] = [];
    private _originalSortItems : ATSortItem[] = [];

    constructor() { }

    ngOnInit(): void {
    }

    drop(event: CdkDragDrop<string[]>) {
        moveItemInArray(this.sortItems, event.previousIndex, event.currentIndex);
    }

    toggleSortIcon(item : ATSortItem){
        item.sortDirection = item.sortDirection == SortDirection.Ascending ?  SortDirection.Descending :  SortDirection.Ascending ;
    }

    onCloseMatMenu(reason : any){
        this.sortItems = _.cloneDeep(this._originalSortItems);
    }

    onSaveClick(){
        this.onSave.emit(this.sortItems);
        this._originalSortItems = _.cloneDeep(this.sortItems);
        this.matMenuTrigger.closeMenu();
    }

    onClearClick(){
        this.onClear.emit();
        this.matMenuTrigger.closeMenu();
    }

}
