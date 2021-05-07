import { Component, AfterContentInit, OnInit, ChangeDetectorRef, ViewChild, ContentChildren, QueryList } from "@angular/core";
import { ATTableHeaderComponent } from "./elements/at-table-header.component";

@Component({
  selector: 'at-assets-table',
  templateUrl: './at-table.component.html',
  styleUrls: ['./at-table.component.scss']
})

export class ATTableComponent implements OnInit, AfterContentInit {

  @ContentChildren(ATTableHeaderComponent) headers : QueryList<ATTableHeaderComponent>;

  constructor(private _cd: ChangeDetectorRef) 
  { 
  }

  ngOnInit(): void {
  }

  ngAfterViewChecked() {
    this._cd.detectChanges();
  }

  ngAfterContentInit(): void {
  }
}
