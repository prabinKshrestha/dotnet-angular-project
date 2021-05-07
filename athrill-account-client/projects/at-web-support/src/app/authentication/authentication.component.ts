import { Component, OnInit } from '@angular/core';
import { ATGlobalStore } from '../shared/services/at-global.store';

@Component({
  selector: 'at-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {

  constructor(
    public atGlobalStore : ATGlobalStore
  ) { }

  ngOnInit(): void {
  }

}
