import { Component, Inject, OnInit } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ATDataTypes, ATDataValueModel, ODataQueryParameters, ContentFormModel, ContentModel, ContentTypeModel } from 'at-models';
import { ATDatasService, CommonHelperService, ContentService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-content-form',
  templateUrl: './content-form.component.html'
})
export class ContentFormComponent implements OnInit {

  public activeModel: ContentFormModel = new ContentFormModel;

  public isAdd: boolean = false;
  public showLoader: boolean = false;

  public contentPlacements: ATDataValueModel[];
  public contentParents: ContentModel[];
  public contentTypes: ContentTypeModel[];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _contentService: ContentService,
    private _atDataService: ATDatasService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<ContentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ContentModel
  ) {
    _commonHelperService.Map<ContentFormModel>(data, this.activeModel);
    this.isAdd = data.Id ? false : true;
  }

  ngOnInit(): void {
    this._getPreDatas();
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      if (this.isAdd) {
        return this._contentService.addContent(this.activeModel);
      } else {
        return this._contentService.updateContent(this.activeModel.ContentId, this.activeModel);
      }
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {
    this._atSnackBarService.show(`Content ${this.isAdd ? 'Added' : 'Updated'} Successfully.`);
    this.dialogRef.close({
      isContentAddEdit: true,
      isAdd: this.isAdd,
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
    let oDataParams: ODataQueryParameters = new ODataQueryParameters();
    oDataParams.Nopaging = true;
    let subscribtions: [
      Observable<ContentTypeModel[]>,
      Observable<ContentModel[]>,
      Observable<ATDataValueModel[]>
    ] = [
        this._contentService.getAllContentTypes(),
        this._contentService.getContents(oDataParams),
        this._atDataService.getATDataValuesByType(ATDataTypes.ContentPlacement)
      ];
    forkJoin(subscribtions).subscribe((data: [ContentTypeModel[], ContentModel[], ATDataValueModel[]]) => {
      this.contentTypes = data[0];
      this.contentParents = data[1];
      if (!this.isAdd) {
        this.contentParents = this.contentParents.filter(x => x.ParentId != this.activeModel.ContentId && x.ContentId != this.activeModel.ContentId);
      }
      this.contentPlacements = data[2];
      this.showLoader = false;
    });
  }

}
