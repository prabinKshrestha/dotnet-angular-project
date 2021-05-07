import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { forkJoin, Observable, of } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmailSettingAddModel, EmailSettingModel, EmailSettingUpdateModel } from 'at-models';
import { EmailSettingService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-email-setting-form',
  templateUrl: './email-setting-form.component.html'
})
export class EmailSettingFormComponent implements OnInit {

  activeModel: EmailSettingModel;

  showLoader: boolean = false;

  public isAdd: boolean = false;

  constructor(
    private _emailSettingService: EmailSettingService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<EmailSettingFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EmailSettingModel
  ) {
    this.isAdd = data.Id ? false : true;
    this.activeModel = data;
  }

  ngOnInit(): void {
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      if (this.isAdd) {
        return this._emailSettingService.addEmailSetting(this.activeModel as EmailSettingAddModel);
      } else {
        return this._emailSettingService.updateEmailSetting(this.activeModel.Id, this.activeModel as EmailSettingUpdateModel);
      }
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {
    if (this.isAdd) {
      this._atSnackBarService.show("Email Added Successfully.");
    } else {
      this._atSnackBarService.show("Email Updated Successfully.");
    }
    this.dialogRef.close({
      isEmailSettingAddEdit: true,
      isAdd: this.isAdd,
      data: value
    });
  }

  processFailure(value: any) {
  }

  processCancel(value: boolean) {
    this.dialogRef.close();
  }

}
