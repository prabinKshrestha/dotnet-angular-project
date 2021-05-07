import { Component, AfterContentInit, OnInit, ChangeDetectorRef, Input } from "@angular/core";

@Component({
    selector: 'at-assets-table-header',
    template: ''
})

export class ATTableHeaderComponent implements OnInit {

    @Input() width: number;
    @Input() label: string;

    ngOnInit(): void {
    }
}
