import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { ATDialogService, ATSnackBarService } from 'at-assets';
import { TeamCategoryMemberLinkModel, TeamCategoryModel, TeamMemberOrientationUpdateModel } from 'at-models';
import { CommonHelperService, TeamCategoryService, TeamMemberService } from 'at-services';

@Component({
  selector: 'at-team-member-orientation',
  templateUrl: './team-member-orientation.component.html',
  styles: [
    `
      .team-member-orientation-wrapper{
          padding: 25px 20px;  
      }
      .drag-and-drop-holder{
          margin-bottom: 20px;
      }
      .drag-and-drop-holder:last-child{
          margin-bottom: 0px;
      } 
      .drag-and-drop-holder h3{
          margin: 0px;
          font-weight: 500;
      } 
    `
  ]
})
export class TeamMemberOrientationComponent implements OnInit {

  public teamCategories: TeamCategoryModel[] = [];
  public changedItemCollections: Array<{ TeamCategoryId: number, TeamCategoryMemberLinks: TeamCategoryMemberLinkModel[] }> = [];
  public isContentLoading: boolean = false;
  public disableSaveAndClear: boolean = true;

  constructor(
    private _atDialogService: ATDialogService,
    private _commonHelperService: CommonHelperService,
    private _teamCategoryService: TeamCategoryService,
    private _teamMemberService: TeamMemberService,
    private _atSnackBarService: ATSnackBarService
  ) { }

  ngOnInit(): void {
    this._getRecords();
  }

  private _getRecords() {
    this.isContentLoading = true;
    this._teamCategoryService.getTeamCategories({ Orderby: 'Orientation ASC', Expand: 'TeamCategoryMemberLinks.TeamMember' }).subscribe(res => {
      this.teamCategories = res;
      this.teamCategories.forEach(x => {
        x.TeamCategoryMemberLinks = _.sortBy(x.TeamCategoryMemberLinks, ["TeamMemberOrientation"]);
      });
      this.changedItemCollections = this.teamCategories.map(x => { return { TeamCategoryId: x.TeamCategoryId, TeamCategoryMemberLinks: x.TeamCategoryMemberLinks } });
      this.isContentLoading = false;
      this.disableSaveAndClear = true;
    });
  }

  onMove(teamCategoryId: number, items: TeamCategoryMemberLinkModel[]) {
    this.disableSaveAndClear = false;
    this.changedItemCollections.find(x => x.TeamCategoryId == teamCategoryId).TeamCategoryMemberLinks = items;
  }

  onClear() {
    this.teamCategories = this._commonHelperService.clone(this.teamCategories);
    this.changedItemCollections = this.teamCategories.map(x => { return { TeamCategoryId: x.TeamCategoryId, TeamCategoryMemberLinks: x.TeamCategoryMemberLinks } });
    this.disableSaveAndClear = true;
  }

  onSave() {
    this.isContentLoading = true;
    let sendVal: TeamMemberOrientationUpdateModel[] = [];
    this.changedItemCollections.forEach(x => {
      x.TeamCategoryMemberLinks.forEach(y => {
        sendVal.push({
          TeamCategoryId: x.TeamCategoryId,
          TeamMemberId: y.TeamMemberId
        });
      });
    });
    this._teamMemberService.updateTeamMemberPosition(sendVal).subscribe({
      next: () => {
        this._atSnackBarService.show("Successfully Updated Orientation");
        this._getRecords();
      },
      error: (error) => {
        this.isContentLoading = false;
        this._atDialogService.openAlertDialog(this._commonHelperService.parseError(error));
      }
    })
  }
}