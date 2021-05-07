import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UsersLayoutComponent } from './users-layout.component';
import { UserTrackRecordGuard } from './users.guard';
import { UserAuthTrackComponent } from './views/user-auth-track/user-auth-track.component';
import { UserGeneralComponent } from './views/user-general/user-general.component';


const routes: Routes = [
  {
    path: '',
    component: UsersLayoutComponent,
    children: [
      {
        path: 'general',
        component: UserGeneralComponent,
        pathMatch: 'full'
      },
      {
        path: 'track',
        component: UserAuthTrackComponent,
        pathMatch: 'full',
        canActivate: [UserTrackRecordGuard]
      },
      {
        path: '',
        redirectTo: 'general'
      },
      {
        path: '**',
        component: UserGeneralComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
