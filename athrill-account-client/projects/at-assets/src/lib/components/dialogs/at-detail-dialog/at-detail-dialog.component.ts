import { Component, EventEmitter, Output, Input } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'at-assets-detail-dialog',
  templateUrl: './at-detail-dialog.component.html',
  styleUrls: ['./../at-dialog.component.scss']
})
export class ATDetialDialogComponent {

  @Input() title : string = "";
  @Input() isManualClose : boolean = false;
  @Output() onClose : EventEmitter<boolean> = new EventEmitter();

  constructor(public dialogRef: MatDialogRef<ATDetialDialogComponent>) 
  {  }

  onDeclineButtonClick(): void {
    /*
      NOTE: isManualClose is passed true if have to manually closed from that end, so that we can customize the data to send to dialog opened areas. 
      This code is written so that we should not write many codes in all individual components.
    */
    if(!this.isManualClose){
      this.dialogRef.close();
    }
    this.onClose.emit();
  }

}