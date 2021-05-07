import { Component, AfterContentInit, OnInit, ChangeDetectorRef, Input } from "@angular/core";

@Component({
    selector: 'at-assets-view-list-item',
    template: ''
})

export class ATViewListItemComponent implements OnInit {

    @Input() name: string;
    @Input() route: string;

    ngOnInit(): void {
    }
}
