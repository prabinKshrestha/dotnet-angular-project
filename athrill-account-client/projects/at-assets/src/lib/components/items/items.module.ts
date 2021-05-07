import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TreeModule } from '@circlon/angular-tree-component';

import { ATMenusComponent } from './at-menus/at-menus.component';
import { ATAssetsSharedModule } from '../../at-assets-shared.module';
import { ATLoaderComponent } from './at-loader/at-loader.component';
import { ATHtmlContentComponent } from './at-html-content/at-html-content';
import { ATDragAndDropComponent } from './at-drag-and-drop/at-drag-and-drop.component';
import { ATButtonTextComponent } from './buttons/at-button-text/at-button-text.component';
import { ATButtonIconComponent } from './buttons/at-button-icon/at-button-icon.component';
import { ATIconFontAwesomeComponent } from './icons/at-icon-fa/at-icon-fa.component';
import { ATIconMatComponent } from './icons/at-icon-mat/at-icon-mat.component';
import { ATTreeDNDComponent } from './at-tree-dnd/at-tree-dnd.component';


const ItemsComponentCollection = [
  ATMenusComponent,
  ATLoaderComponent,
  ATHtmlContentComponent,
  ATDragAndDropComponent,
  ATButtonTextComponent,
  ATButtonIconComponent,
  ATIconFontAwesomeComponent,
  ATIconMatComponent,
  ATTreeDNDComponent
];

@NgModule({
  declarations: [
      ...ItemsComponentCollection
  ],
  imports: [
      CommonModule,
      ATAssetsSharedModule,
      TreeModule
  ],
  exports: [
      ...ItemsComponentCollection
  ]
})
export class ItemsModule { }
