import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmailSettingsRoutingModule } from './email-settings-routing.module';
import { EmailSettingsLayoutComponent } from './email-settings-layout.component';
import { SharedModule } from '../../shared/shared.module';
import { EmailSettingGeneralComponent } from './views/email-setting-general/email-setting-general.component';
import { EmailSettingFormComponent } from './views/email-setting-general/elements/email-setting-form/email-setting-form.component';
import { EmailSettingDetailComponent } from './views/email-setting-general/elements/email-setting-detail/email-setting-detail.component';


@NgModule({
  declarations: [EmailSettingsLayoutComponent, EmailSettingGeneralComponent, EmailSettingFormComponent, EmailSettingDetailComponent],
  imports: [
    CommonModule,
    EmailSettingsRoutingModule,
    SharedModule
  ]
})
export class EmailSettingsModule { }
