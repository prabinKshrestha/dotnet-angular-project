import { Injectable } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: 'root',
})

export class ATSnackBarService {

    constructor(private _snackBar: MatSnackBar) { }

    show(message: string) {
        this._snackBar.open(message, "", {
            duration: 3000,
            horizontalPosition: 'center'
        });
    }
}