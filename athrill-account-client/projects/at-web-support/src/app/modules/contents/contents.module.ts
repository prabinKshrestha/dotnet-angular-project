import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContentsRoutingModule } from './contents-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { ContentsLayoutComponent } from './contents-layout.component';
import { ContentGeneralComponent } from './views/content-general/content-general.component';
import { ContentFormComponent } from './views/content-general/elements/content-form/content-form.component';
import { ContentDetailComponent } from './views/content-general/elements/content-detail/content-detail.component';
import { ContentTreeComponent } from './views/content-tree/content-tree.component';


@NgModule({
  declarations: [ContentsLayoutComponent, ContentGeneralComponent, ContentFormComponent, ContentDetailComponent, ContentTreeComponent],
  imports: [
    CommonModule,
    ContentsRoutingModule,
    SharedModule
  ]
})
export class ContentsModule { }
