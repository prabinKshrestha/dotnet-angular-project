import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ATDataValueModel,SiteSettingUpdateModel, SiteSettingModel} from 'at-models';
import { CommonHelperService, SiteSettingService} from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-site-setting-edit',
  templateUrl: './site-setting-edit.component.html'
})
export class SiteSettingEditComponent implements OnInit {

  activeModel: SiteSettingUpdateModel = new SiteSettingUpdateModel;

  showLoader: boolean = false;
  public Genders: ATDataValueModel[];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _siteSettingService: SiteSettingService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<SiteSettingEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SiteSettingModel
  ) {
    _commonHelperService.Map<SiteSettingUpdateModel>(data, this.activeModel);
  }

  ngOnInit(): void {
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      return this._siteSettingService.updateSiteSetting(this.activeModel);
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {      
    this._atSnackBarService.show("Site Setting Updated Successfully.");
    this.dialogRef.close({
      isSiteSettingEdit: true,
      data: value
    });
  }

  processFailure(value: any) {
  }

  processCancel(value: boolean) {
    this.dialogRef.close();
  }

}
