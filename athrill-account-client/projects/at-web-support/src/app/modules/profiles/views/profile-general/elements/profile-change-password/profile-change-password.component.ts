import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MatDialogRef } from '@angular/material/dialog';
import { ATDataValueModel, ChangePasswordModel } from 'at-models';
import { AuthenticationService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
    selector: 'at-profile-change-password',
    templateUrl: './profile-change-password.component.html'
})
export class ProfileChangePasswordComponent implements OnInit {

    activeModel: ChangePasswordModel = new ChangePasswordModel;

    showLoader: boolean = false;

    constructor(
        private _authService: AuthenticationService,
        private _atSnackBarService: ATSnackBarService,
        public dialogRef: MatDialogRef<ProfileChangePasswordComponent>
    ) {
    }

    ngOnInit(): void {
    }

    isValid(): boolean {
        return this.activeModel.NewPassword == this.activeModel.NewConfirmationPassword;
    }

    processForm(): Observable<any> {
        if (this.isValid()) {
            return this._authService.changePassword(this.activeModel)
        }
    }

    processProcessing(value: boolean) {
        this.showLoader = value;
    }

    processSubmission(value: boolean) {
    }

    processSuccess(value: any) {
        this._atSnackBarService.show("Password Changed Successfully.");
        this.dialogRef.close({
            isPasswordChange: true,
            data: value
        });
    }

    processFailure(value: any) {
    }

    processCancel(value: boolean) {
        this.dialogRef.close();
    }
}
