import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'at-assets-icon-fa',
  template: `
    <span 
      class="at-assets-icon at-assets-icon-fa {{ icon }}" 
      aria-hidden="true" 
      [ngClass]="classes"
      [style.fontSize.px]="fontSize"
      [style.height.px]="fontSize"
      [style.width.px]="fontSize"
      [style.lineHeight.px]="fontSize"
    ></span>
  `,
  styles: []
})
export class ATIconFontAwesomeComponent implements OnInit {

  @Input() icon: string = "";
  @Input() classes: string = null;
  @Input() fontSize: number = 20;

  constructor() { }

  ngOnInit(): void {
  }

}
