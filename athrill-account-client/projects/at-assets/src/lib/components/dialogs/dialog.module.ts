import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ATAssetsSharedModule } from '../../at-assets-shared.module';
import { ItemsModule } from '../items/items.module';
import { ATConfirmationDialogComponent } from './at-confirmation-dialog/at-confirmation-dialog.component';
import { ATDetialDialogComponent } from './at-detail-dialog/at-detail-dialog.component';
import { ATAddEditDialogComponent } from './at-add-edit-dialog/at-add-edit-dialog.component';
import { ATAlertDialogComponent } from './at-alert-dialog/at-alert-dialog.component';

const ComponentCollection = [
    ATConfirmationDialogComponent,
    ATDetialDialogComponent,
    ATAddEditDialogComponent,
    ATAlertDialogComponent
];

@NgModule({
    declarations: [
        ...ComponentCollection
    ],
    imports: [
        CommonModule,
        ATAssetsSharedModule,
        ItemsModule
    ],
    exports: [
        ...ComponentCollection
    ]
})
export class DialogModule { }
