import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ATAssetsSharedModule } from '../../at-assets-shared.module';
import { ItemsModule } from '../items/items.module';
import { ATGridComponent } from './at-grid/at-grid.component';
import { ATTableComponent } from './at-table/at-table.component';
import { ATTableHeaderComponent } from './at-table/elements/at-table-header.component';
import { ATSortComponent } from './at-sorts/at-sort.component';
import { ATFilterComponent } from './at-filters/at-filter.component';
import { ATFormsModule } from '../forms/at-forms.module';

const ComponentCollection = [
    ATGridComponent,
    ATTableComponent,
    ATTableHeaderComponent,
    ATSortComponent,
    ATFilterComponent
];

@NgModule({
    declarations: [
        ...ComponentCollection
    ],
    imports: [
        CommonModule,
        FormsModule,
        ATFormsModule,
        ATAssetsSharedModule,
        ItemsModule
    ],
    exports: [
        ...ComponentCollection
    ]
})
export class TableModule { }
