import { ContentChildren,ChangeDetectorRef, QueryList, Component, Input, OnInit   } from '@angular/core';
import { ATViewListItemComponent } from './elements/at-view-list-item.component';

@Component({
  selector: 'at-assets-main-layout',
  templateUrl: './at-main-layout.component.html',
  styleUrls: ['./at-main-layout.component.scss']
})
export class ATMainlayoutComponent implements OnInit {

  @Input() title : string = "";
  @ContentChildren(ATViewListItemComponent) viewItems : QueryList<ATViewListItemComponent>;

  constructor(private _cd: ChangeDetectorRef) 
  { 
  }

  ngOnInit(): void {
  }

  ngAfterViewChecked() {
    this._cd.detectChanges();
  }
}
