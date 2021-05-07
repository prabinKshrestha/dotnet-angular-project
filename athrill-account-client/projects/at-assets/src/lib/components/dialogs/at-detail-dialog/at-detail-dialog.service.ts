import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { ComponentRef, Injectable, TemplateRef } from '@angular/core';
import { ComponentType } from "@angular/cdk/portal";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class ATDetailDialogService {

    /**
     * **Note:** Subscribe this behavior to get the object passed when the dialog is closed.
    */
    public dialogClosed : BehaviorSubject<any> = new BehaviorSubject<any>(null);

    private _dialogRef: MatDialogRef<any>;

    constructor(
        public dialog: MatDialog) { }

    public openDetailDialog<T>(componentOrTemplateRef: ComponentType<T> | TemplateRef<T>, data : any) {
        this._dialogRef = this.dialog.open(componentOrTemplateRef, {
            data: data,
            panelClass: 'at-detail-dialog',
            disableClose: true
        });

        this._dialogRef.beforeClosed().subscribe(res => {
            this.dialogClosed.next(res);
        });
    }

}