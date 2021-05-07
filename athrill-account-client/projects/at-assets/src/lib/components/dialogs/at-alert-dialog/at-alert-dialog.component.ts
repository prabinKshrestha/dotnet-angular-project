import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'at-assets-alert-dialog',
  templateUrl: './at-alert-dialog.component.html',
  styleUrls: ['./../at-dialog.component.scss']
})
export class ATAlertDialogComponent {

  constructor(public dialogRef: MatDialogRef<ATAlertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) 
  {  }
  
  onCloseClick(): void {
    this.dialogRef.close(true);
  }

}
