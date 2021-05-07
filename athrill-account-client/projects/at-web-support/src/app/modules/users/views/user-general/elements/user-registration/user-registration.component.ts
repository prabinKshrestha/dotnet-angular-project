import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ATDataTypes, ATDataValueModel, UserRegistrationModel, UserRoleModel } from 'at-models';
import { ATDatasService, AuthenticationService, UserService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-user-registration',
  templateUrl: './user-registration.component.html'
})
export class UserRegistrationComponent implements OnInit {

  activeModel: UserRegistrationModel;

  showLoader: boolean = false;

  public Genders: ATDataValueModel[];
  public UserRoles: UserRoleModel[];

  constructor(
    private _userService: UserService,
    private _atDataService: ATDatasService,
    private _authService: AuthenticationService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<UserRegistrationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserRegistrationModel
  ) {
    this.activeModel = data;
  }

  ngOnInit(): void {
    this._getPreDatas();
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      return this._authService.registerUser(this.activeModel);
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {
    this._atSnackBarService.show("User Registered Successfully.");
    this.dialogRef.close({
      isUserRegistration: true
    });
  }

  processFailure(value: any) {
  }

  processCancel(value: boolean) {
    this.dialogRef.close();
  }

  private _getPreDatas() {
    this.showLoader = true;
    let subscribtions: [
      Observable<ATDataValueModel[]>,
      Observable<UserRoleModel[]>
    ] = [
        this._atDataService.getATDataValuesByType(ATDataTypes.Gender),
        this._userService.getUserRoles()
      ];
    forkJoin(subscribtions).subscribe((data: [ATDataValueModel[], UserRoleModel[]]) => {
      this.Genders = data[0];
      this.UserRoles = data[1];
      this.showLoader = false;
    });
  }

}
