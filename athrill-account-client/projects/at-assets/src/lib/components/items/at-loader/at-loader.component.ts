import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'at-assets-loader',
  templateUrl: './at-loader.component.html',
  styleUrls: ['./at-loader.component.scss']
})
export class ATLoaderComponent implements OnInit {

  @Input() color : 'accent' | 'primary' | 'basic' = 'accent';

  constructor() { }

  ngOnInit(): void {
  }

}
