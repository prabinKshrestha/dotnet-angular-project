import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'at-assets-html-content',
  template: `
    <div class="html-content-wrapper">
        <div [innerHtml]="content"></div>
    </div>
  `,
  styles: [],
  encapsulation: ViewEncapsulation.ShadowDom
})
export class ATHtmlContentComponent implements OnInit {

  @Input() content : string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
