import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'at-assets-icon-mat',
  template: `
    <mat-icon 
      class="at-assets-icon at-assets-icon-mat" 
      [ngClass]="classes"
      [style.fontSize.px]="fontSize"
      [style.height.px]="fontSize"
      [style.width.px]="fontSize"
      [style.lineHeight.px]="fontSize"
    >
      {{ icon }}
    </mat-icon>
  `,
  styles: []
})
export class ATIconMatComponent implements OnInit {

  @Input() icon: string = "";
  @Input() classes: string = null;
  @Input() fontSize: number = 24;

  constructor() { }

  ngOnInit(): void {
  }

}
