import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ATAssetsSharedModule } from '../../at-assets-shared.module';
import { ItemsModule } from '../items/items.module';

import { ATMainlayoutComponent } from './at-main-layout/at-main-layout.component';
import { ATContentLayoutComponent } from './at-content-layout/at-content-layout.component';
import { ATViewListItemComponent } from './at-main-layout/elements/at-view-list-item.component';

const LayoutComponentCollection = [
    ATMainlayoutComponent,
    ATContentLayoutComponent,
    ATViewListItemComponent
];

@NgModule({
    declarations: [
        ...LayoutComponentCollection
    ],
    imports: [
        CommonModule,
        ATAssetsSharedModule,
        ItemsModule
    ],
    exports: [
        ...LayoutComponentCollection
    ]
})
export class LayoutsModule { }
