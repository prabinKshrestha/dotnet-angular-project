import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EmailSettingsLayoutComponent } from './email-settings-layout.component';
import { EmailSettingGeneralComponent } from './views/email-setting-general/email-setting-general.component';


const routes: Routes = [
  {
    path: '',
    component: EmailSettingsLayoutComponent,
    children: [
      {
        path: 'general',
        component: EmailSettingGeneralComponent,
        pathMatch: 'full'
      },
      {
        path: '',
        redirectTo: 'general'
      },
      {
        path: '**',
        component: EmailSettingGeneralComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmailSettingsRoutingModule { }
