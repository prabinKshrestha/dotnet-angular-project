import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmailSettingModel } from 'at-models';

@Component({
  selector: 'at-email-setting-detail',
  templateUrl: './email-setting-detail.component.html'
})
export class EmailSettingDetailComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: EmailSettingModel) { }

  ngOnInit(): void {
  }

}
