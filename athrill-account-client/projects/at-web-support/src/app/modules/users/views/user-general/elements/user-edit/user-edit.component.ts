import { Component, Inject, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserRoleModel, UserEditModel } from 'at-models';
import { UserService } from 'at-services';
import { ATSnackBarService } from 'at-assets';
import { forkJoin, Observable } from 'rxjs';

@Component({
    selector: 'at-user-edit',
    templateUrl: './user-edit.component.html'
  })

  export class UserEditComponent implements OnInit {

    activeModel: UserEditModel;

    showLoader: boolean = false;

    public UserRoles: UserRoleModel[];

    constructor(
      private _userService: UserService,
      private _atSnackBarService: ATSnackBarService,
      public dialogRef: MatDialogRef<UserEditComponent>,
      @Inject(MAT_DIALOG_DATA) public data: UserEditModel
    ){
      this.activeModel = data;
    }
    ngOnInit(): void{
      this._getPreDatas();
    }

    isValid(): boolean{
      return true;
    }

    processForm(): Observable<any>{
      if (this.isValid()) {
        return this._userService.updateRegisteredUser(this.activeModel.UserId, this.activeModel);
      } 
    }

    processProcessing(value: boolean){
      this.showLoader = value;
    }

    processSubmission(value: boolean){
    }

    processSuccess(value: any){
      this._atSnackBarService.show("User Updated Successfully")
      this.dialogRef.close({
        isRegisteredUserUpdated : true
      });
    }

    processFailure(value: boolean){
      this.dialogRef.close();
    }

    processCancel(value: boolean){
      this.dialogRef.close();
    }

    private _getPreDatas() {
      this.showLoader = true;
      let subscribtions: [
        Observable<UserRoleModel[]>
      ] = [
        this._userService.getUserRoles()
      ];
      forkJoin(subscribtions).subscribe((data: [UserRoleModel[]]) =>{
        this.UserRoles = data[0];
        this.showLoader = false;
      });
    }
  }