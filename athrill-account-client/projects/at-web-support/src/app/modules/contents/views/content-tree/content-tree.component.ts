import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';
import { ContentTreeModel } from 'at-models';
import { CommonHelperService, ContentService } from 'at-services';
import { ATDialogService, ATSnackBarService } from 'at-assets';

@Component({
  selector: 'at-content-tree',
  templateUrl: './content-tree.component.html',
  styles: [
    `
      .tree-dnd-holder {
        padding: 25px 20px;
      }
      ::ng-deep .mat-tree-dnd-wrapper{
        overflow: auto;
        height: calc(100vh - 245px);
      } 
    `
  ]
})
export class ContentTreeComponent implements OnInit {

  public items: ContentTreeModel[] = [];
  public changedItems: ContentTreeModel[] = [];

  public isContentLoading: boolean = false;

  constructor(
    private _atDialogService: ATDialogService,
    private _commonHelperService: CommonHelperService,
    private _contentService: ContentService,
    private _atSnackBarService: ATSnackBarService
  ) { }

  ngOnInit(): void {
    this._getRecords();
  }

  private _getRecords() {
    this.isContentLoading = true;
    this._contentService.getContentTrees().subscribe(res => {
      this.items = res;
      this.changedItems = [];
      this.isContentLoading = false;
    });
  }

  onMove(items: ContentTreeModel[]) {
    this.changedItems = items;
  }

  onClear() {
    this.items = _.cloneDeep(this.items);
    this.changedItems = [];
  }

  onSave() {
    this.isContentLoading = true;
    this._contentService.updateContentTrees(this.changedItems).subscribe({
      next: () => {
        this._atSnackBarService.show("Successfully Updated Tree Structure of the Contents")
        this._getRecords();
      },
      error: (error) => {
        this.isContentLoading = false;
        this._atDialogService.openAlertDialog(this._commonHelperService.parseError(error));
      }
    })
  }

}
