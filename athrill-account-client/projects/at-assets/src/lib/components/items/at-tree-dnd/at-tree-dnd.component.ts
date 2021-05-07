import { Component, ContentChild, ElementRef, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { KEYS, TREE_ACTIONS } from '@circlon/angular-tree-component';
import * as _ from 'lodash';

@Component({
    selector: 'at-assets-tree-dnd',
    templateUrl: './at-tree-dnd.component.html',
    styleUrls: ['./at-tree-dnd.component.scss']
})
export class ATTreeDNDComponent implements OnInit {

    @ContentChild('treeDNDItem') treeDNDItem: TemplateRef<ElementRef>;

    @Input() set nodes(value : any[]){
        this.nodesToProcess = _.cloneDeep(value);
    };
    @Input() displayField: string = null;
    @Input() childrenField: string = null;
    
    @Output() onMove: EventEmitter<any[]> = new EventEmitter();

    nodesToProcess : any[] = [];

    dragAndDropOptions = {
        displayField: this.displayField,
        childrenField: this.childrenField,
        allowDrag: (node) => {
            return true;
        },
        allowDrop: (node) => {
            return true;
        },
        actionMapping: {
            mouse: {
                dblClick: (tree, node, $event) => {
                    if (node.hasChildren) {
                        TREE_ACTIONS.TOGGLE_EXPANDED(tree, node, $event);
                    }
                }
            },
            keys: {
                [KEYS.ENTER]: (tree, node, $event) => {
                    node.expandAll();
                }
            }
        },
    };

    constructor(
    ) { }

    ngOnInit(): void {
    }

    ngAfterViewChecked(){
        this.dragAndDropOptions.displayField = this.displayField;
        this.dragAndDropOptions.childrenField = this.childrenField;
    }

    onMoveNode($event){
        this.onMove.emit(this.nodesToProcess);
    }

}
