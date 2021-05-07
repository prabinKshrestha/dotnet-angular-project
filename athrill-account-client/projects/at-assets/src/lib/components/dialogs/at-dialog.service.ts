import { MatDialog } from "@angular/material/dialog";
import { Injectable } from '@angular/core';
import { ATConfirmationDialogComponent } from "./at-confirmation-dialog/at-confirmation-dialog.component";
import { ATAlertDialogComponent } from "./at-alert-dialog/at-alert-dialog.component";

@Injectable({
    providedIn: 'root',
})

export class ATDialogService {

    constructor(public dialog: MatDialog) { }

    public openConfirmationDialog(bodyText : string, title : string = "Please Confirm", headerIcon : string = "", acceptButtonText : string = "Yes", declineButtonText : string = "No") {
        let dialogRef = this.dialog.open(ATConfirmationDialogComponent, {
            data: {
                body : bodyText,
                title : title,
                headerIcon : headerIcon,
                acceptButtonText : acceptButtonText,
                declineButtonText : declineButtonText,
            },
            panelClass: 'at-confirmation-dialog',
            disableClose: true
        });
        return dialogRef.afterClosed();
    }

    public openAlertDialog(bodyText : string, title : string = "Alert", acceptButtonText : string = "Ok") {
        let dialogRef = this.dialog.open(ATAlertDialogComponent, {
            data: {
                body : bodyText,
                title : title,
                acceptButtonText : acceptButtonText
            },
            panelClass: 'at-alert-dialog',
            disableClose: true
        });
        return dialogRef.afterClosed();
    }

}