import { MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Injectable, TemplateRef } from '@angular/core';
import { ComponentType } from "@angular/cdk/portal";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class ATAddEditDialogService {

    /**
     * **Note:** Subscribe this behavior to get the object passed when the dialog is closed.
    */
    public dialogClosed : BehaviorSubject<any> = new BehaviorSubject<any>(null);

    private _dialogRef: MatDialogRef<any>;

    constructor(
        public dialog: MatDialog) { }

    public openAddEditDialog<T>(componentOrTemplateRef: ComponentType<T> | TemplateRef<T>, data : any) {
        this._dialogRef = this.dialog.open(componentOrTemplateRef, {
            data: data,
            panelClass: 'at-add-edit-dialog',
            disableClose: true
        });

        this._dialogRef.beforeClosed().subscribe(res => {
            this.dialogClosed.next(res);
        });
    }

}