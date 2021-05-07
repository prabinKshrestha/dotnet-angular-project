import { NgModule } from '@angular/core';

import { LayoutsModule } from './components/layouts/layouts.module';
import { ItemsModule } from './components/items/items.module';
import { DialogModule } from './components/dialogs/dialog.module';
import { TableModule } from './components/tables/table.module';
import { ATFormsModule } from './components/forms/at-forms.module';

const assetModules = [
  LayoutsModule,
  ItemsModule,
  DialogModule,
  TableModule,
  ATFormsModule
]

@NgModule({
  declarations: [],
  imports: [
    ...assetModules
  ],
  exports: [
    ...assetModules
  ]
})
export class AtAssetsModule { }
