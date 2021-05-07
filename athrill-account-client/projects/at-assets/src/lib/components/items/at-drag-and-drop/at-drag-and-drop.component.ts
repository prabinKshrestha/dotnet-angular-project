import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, ContentChild, ElementRef, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { CommonHelperService } from 'at-services';

@Component({
    selector: 'at-assets-drag-and-drop',
    templateUrl: './at-drag-and-drop.component.html',
    styleUrls: ['./at-drag-and-drop.component.scss']
})
export class ATDragAndDropComponent implements OnInit {

    @ContentChild('dragAndDropItem') dragAndDropItem: TemplateRef<ElementRef>;

    @Input() set items(value : any[]){
        this.itemsToProcess = this._commonHelperService.clone(value);
    }
    
    @Output() onMove: EventEmitter<any[]> = new EventEmitter();

    public itemsToProcess: any[] = [];

    constructor(
        private _commonHelperService : CommonHelperService
    ) { }

    ngOnInit(): void {
    }

    drop(event: CdkDragDrop<any[]>) {
        moveItemInArray(this.itemsToProcess, event.previousIndex, event.currentIndex);
        this.onMove.emit(this.itemsToProcess);
    }

}
