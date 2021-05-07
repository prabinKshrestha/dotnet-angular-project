import { Component, OnInit, Input,ContentChild, ElementRef, TemplateRef, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'at-assets-content-layout',
  templateUrl: './at-content-layout.component.html',
  styleUrls: ['./at-content-layout.component.scss']
})
export class ATContentLayoutComponent implements OnInit {
  
  @ContentChild('headerRightContents') headerRightContents: TemplateRef<ElementRef>;

  @Input() title : "";

  constructor(private cd : ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  ngAfterViewChecked(): void {
      this.cd.detectChanges();
  }

}
