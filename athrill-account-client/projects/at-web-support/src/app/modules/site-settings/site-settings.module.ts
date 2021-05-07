import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SiteSettingsRoutingModule } from './site-settings-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { SiteSettingsLayoutComponent } from './site-settings-layout.component';
import { SiteSettingGeneralComponent } from './views/site-setting-general/site-setting-general.component';
import { SiteSettingEditComponent } from './views/site-setting-general/elements/site-setting-edit/site-setting-edit.component';


@NgModule({
  declarations: [SiteSettingsLayoutComponent, SiteSettingGeneralComponent, SiteSettingEditComponent],
  imports: [
    CommonModule,
    SiteSettingsRoutingModule,
    SharedModule
  ]
})
export class SiteSettingsModule { }
