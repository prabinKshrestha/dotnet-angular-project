import { Component, Inject, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamCategoryService } from 'at-services';
import { ATSnackBarService } from 'at-assets';
import { TeamCategoryAddModel, TeamCategoryModel, TeamCategoryUpdateModel } from 'at-models';

@Component({
  selector: 'at-team-category-form',
  templateUrl: './team-category-form.component.html'
})
export class TeamCategoryFormComponent implements OnInit {

  activeModel: TeamCategoryModel;

  showLoader: boolean = false;

  public isAdd: boolean = false;

  constructor(
    private _teamCategoryService: TeamCategoryService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<TeamCategoryFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TeamCategoryModel
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
        return this._teamCategoryService.addTeamCategory(this.activeModel as TeamCategoryAddModel);
      } else {
        return this._teamCategoryService.updateTeamCategory(this.activeModel.Id, this.activeModel as TeamCategoryUpdateModel);
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
      this._atSnackBarService.show("Team Category Added Successfully.");
    } else {
      this._atSnackBarService.show("Team Category Updated Successfully.");
    }
    this.dialogRef.close({
      isTeamCategoryAddEdit: true,
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
