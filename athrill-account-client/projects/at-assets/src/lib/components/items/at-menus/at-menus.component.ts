import { Component, Input, OnInit } from '@angular/core';
import { ATMenuItem } from './at-menus.model';

@Component({
  selector: 'at-assets-menus',
  templateUrl: './at-menus.component.html',
  styleUrls: ['./at-menus.component.scss']
})
export class ATMenusComponent implements OnInit {

  @Input() menuItems : ATMenuItem[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
