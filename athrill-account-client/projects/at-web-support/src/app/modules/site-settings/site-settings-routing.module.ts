import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SiteSettingsLayoutComponent } from './site-settings-layout.component';
import { SiteSettingGeneralComponent } from './views/site-setting-general/site-setting-general.component';


const routes: Routes = [
    {
        path: '',
        component: SiteSettingsLayoutComponent,
        children : [
          {
            path: 'general',
            component: SiteSettingGeneralComponent,
            pathMatch: 'full'
          },
          {
            path: '',
            redirectTo: 'general'
          },
          {
            path: '**',
            component: SiteSettingGeneralComponent
          }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteSettingsRoutingModule { }
