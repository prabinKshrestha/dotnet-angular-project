import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon'
import { MatButtonModule } from '@angular/material/button'
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatMenuModule } from '@angular/material/menu';
import {MatTableModule} from '@angular/material/table';
import {MatDialogModule} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';

import { AtAssetsModule } from 'at-assets';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { AtComponentsModule } from 'at-components';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

const AngularMaterialModules = [
  MatToolbarModule,
  MatSidenavModule,
  MatIconModule,
  MatButtonModule,
  MatListModule,
  MatTabsModule,
  MatCardModule,
  MatExpansionModule,
  MatPaginatorModule,
  MatMenuModule,
  MatTableModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule ,
  MatDatepickerModule,
  MatNativeDateModule,
  MatSelectModule
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ...AngularMaterialModules,
    AtAssetsModule,
    AtComponentsModule,
    FormsModule
  ],
  exports: [
    ...AngularMaterialModules,
    AtAssetsModule,
    AtComponentsModule,
    FormsModule
  ]
})
export class SharedModule { }
