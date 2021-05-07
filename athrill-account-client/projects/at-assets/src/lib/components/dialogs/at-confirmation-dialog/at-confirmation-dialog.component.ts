import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'at-assets-confirmation-dialog',
  templateUrl: './at-confirmation-dialog.component.html',
  styleUrls: ['./../at-dialog.component.scss']
})
export class ATConfirmationDialogComponent {

  constructor(public dialogRef: MatDialogRef<ATConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) 
  {  }

  onAcceptButtonClick(): void {
    this.dialogRef.close(true);
  }

  onDeclineButtonClick(): void {
    this.dialogRef.close(false);
  }

}
