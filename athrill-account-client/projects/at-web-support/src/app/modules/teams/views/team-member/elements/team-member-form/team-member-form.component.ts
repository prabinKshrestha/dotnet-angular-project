import { Component, Inject, OnInit } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ATDataTypes, ATDataValueModel, ODataQueryParameters, TeamMemberFormModel, ContentModel, ContentTypeModel, TeamCategoryModel, TeamMemberModel } from 'at-models';
import { ATDatasService, CommonHelperService, TeamMemberService, TeamCategoryService } from 'at-services';
import { ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-team-member-form',
  templateUrl: './team-member-form.component.html'
})
export class TeamMemberFormComponent implements OnInit {

  public activeModel: TeamMemberFormModel = new TeamMemberFormModel;

  public isAdd: boolean = false;
  public showLoader: boolean = false;

  public teamCategories: TeamCategoryModel[];

  constructor(
    private _commonHelperService: CommonHelperService,
    private _teamMemberService: TeamMemberService,
    private _teamCategoryService: TeamCategoryService,
    private _atSnackBarService: ATSnackBarService,
    public dialogRef: MatDialogRef<TeamMemberFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TeamMemberModel
  ) {
    _commonHelperService.Map<TeamMemberFormModel>(data, this.activeModel);
    this.isAdd = data.Id ? false : true;
  }

  ngOnInit(): void {
    if(!this.isAdd && this.data.TeamCategoryMemberLinks){
      this.activeModel.TeamCategoryIds = this.data.TeamCategoryMemberLinks.map(x => x.TeamCategoryId);
    }
    this._getPreDatas();
  }

  isValid(): boolean {
    return true;
  }

  processForm(): Observable<any> {
    if (this.isValid()) {
      if (this.isAdd) {
        return this._teamMemberService.addTeamMember(this.activeModel);
      } else {
        return this._teamMemberService.updateTeamMember(this.activeModel.TeamMemberId, this.activeModel);
      }
    }
  }

  processProcessing(value: boolean) {
    this.showLoader = value;
  }

  processSubmission(value: boolean) {
  }

  processSuccess(value: any) {
    this._atSnackBarService.show(`Team Member ${this.isAdd ? 'Added' : 'Updated'} Successfully.`);
    this.dialogRef.close({
      isTeamMemberAddEdit: true,
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
      Observable<TeamCategoryModel[]>
    ] = [
        this._teamCategoryService.getTeamCategories()
      ];
    forkJoin(subscribtions).subscribe((data: [TeamCategoryModel[]]) => {
      this.teamCategories = data[0];
      this.showLoader = false;
    });
  }

}
