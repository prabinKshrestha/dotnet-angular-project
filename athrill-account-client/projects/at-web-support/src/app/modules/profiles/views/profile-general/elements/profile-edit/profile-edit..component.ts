import { Component, Inject, OnInit } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ATDataTypes, ATDataValueModel, ODataQueryParameters, UserModel, UserUpdateModel } from 'at-models';
import { ATDatasService, CommonHelperService, UserService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-profile-edit',
  templateUrl: './profile-edit.component.html'
})
export class ProfileEditComponent implements OnInit {

  activeModel: UserUpdateModel = new UserUpdateModel;

  showLoader: boolean = false;
  public Genders: ATDataValueModel[];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _userService: UserService,
    private _atDataService: ATDatasService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<ProfileEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserModel
  ) {
    _commonHelperService.Map<UserUpdateModel>(data, this.activeModel);
  }

  ngOnInit(): void {
    this._getPreDatas();
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      let oDataParams: ODataQueryParameters = new ODataQueryParameters;
      oDataParams.Expand = "UserLogin, UserRoleLink.UserRole, Gender";
      return this._userService.updateUser(this.activeModel.UserId, this.activeModel, oDataParams);
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {      
    this._atSnackBarService.show("Profile Updated Successfully.");
    this.dialogRef.close({
      isProfileEdit: true,
      data: value
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
      Observable<ATDataValueModel[]>
    ] = [
        this._atDataService.getATDataValuesByType(ATDataTypes.Gender)
      ];
    forkJoin(subscribtions).subscribe((data: [ATDataValueModel[]]) => {
      this.Genders = data[0];
      this.showLoader = false;
    });
  }

}
