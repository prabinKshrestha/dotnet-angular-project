import { Component, Input } from '@angular/core';

@Component({
  selector: 'at-assets-add-edit-dialog',
  templateUrl: './at-add-edit-dialog.component.html',
  styleUrls: ['./../at-dialog.component.scss']
})
export class ATAddEditDialogComponent {

  @Input() title : string = "";

  constructor() 
  {  }

}