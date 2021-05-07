import { Component, OnInit } from '@angular/core';
import { ATDialogService, ATSnackBarService } from 'at-assets';
import { TeamCategoryModel } from 'at-models';
import { CommonHelperService, TeamCategoryService } from 'at-services';

@Component({
  selector: 'at-team-category-orientation',
  templateUrl: './team-category-orientation.component.html',
  styles: [
    `
      .drag-and-drop-holder{
        padding: 25px 20px;  
      }
    `
  ]
})
export class TeamCategoryOrientationComponent implements OnInit {

  public items: TeamCategoryModel[] = [];
  public changedItems: TeamCategoryModel[] = [];
  public isContentLoading: boolean = false;

  constructor(
    private _atDialogService: ATDialogService,
    private _commonHelperService: CommonHelperService,
    private _teamCategoryService: TeamCategoryService,
    private _atSnackBarService: ATSnackBarService
  ) { }

  ngOnInit(): void {
    this._getRecords();
  }

  private _getRecords() {
    this.isContentLoading = true;
    this._teamCategoryService.getTeamCategories({ Orderby: 'Orientation ASC' }).subscribe(res => {
      this.items = res;
      this.changedItems = [];
      this.isContentLoading = false;
    });
  }

  onMove(items: TeamCategoryModel[]) {
    this.changedItems = items;
  }

  onClear() {
    this.items = this._commonHelperService.clone(this.items);
    this.changedItems = [];
  }

  onSave() {
    this.isContentLoading = true;
    this._teamCategoryService.updateTeamCategoryPosition(this.changedItems.map(x => x.Id)).subscribe({
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