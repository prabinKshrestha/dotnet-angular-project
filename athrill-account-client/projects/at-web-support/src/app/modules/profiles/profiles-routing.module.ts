import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProfilesLayoutComponent } from './profiles-layout.component';
import { ProfileUserTrackRecordGuard } from './profiles.guard';
import { ProfileAuthTrackComponent } from './views/profile-auth-track/profile-auth-track.component';
import { ProfileGeneralComponent } from './views/profile-general/profile-general.component';


const routes: Routes = [
  {
    path: '',
    component: ProfilesLayoutComponent,
    children: [
      {
        path: 'general',
        component: ProfileGeneralComponent,
        pathMatch: 'full'
      },
      {
        path: 'track',
        component: ProfileAuthTrackComponent,
        pathMatch: 'full',
        canActivate: [ProfileUserTrackRecordGuard]
      },
      {
        path: '',
        redirectTo: 'general'
      },
      {
        path: '**',
        component: ProfileGeneralComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
