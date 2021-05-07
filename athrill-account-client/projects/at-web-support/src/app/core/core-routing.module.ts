import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CoreComponent } from './core/core.component';
import { EmailSettingGuard, SiteSettingGuard, UserGuard } from './guards/module.guard';


const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      {
        path: 'profile',
        loadChildren: () => import('../modules/profiles/profiles.module').then(m => m.ProfilesModule)
      },
      {
        path: 'users',
        loadChildren: () => import('../modules/users/users.module').then(m => m.UsersModule),
        canActivate: [UserGuard]
      },
      {
        path: 'site-settings',
        loadChildren: () => import('../modules/site-settings/site-settings.module').then(m => m.SiteSettingsModule),
        canActivate: [SiteSettingGuard]
      },
      {
        path: 'email-settings',
        loadChildren: () => import('../modules/email-settings/email-settings.module').then(m => m.EmailSettingsModule),
        canActivate: [EmailSettingGuard]
      },
      {
        path: 'contents',
        loadChildren: () => import('../modules/contents/contents.module').then(m => m.ContentsModule)
      },
      {
        path: 'teams',
        loadChildren: () => import('../modules/teams/teams.module').then(m => m.TeamsModule)
      },
      {
        path: '',
        loadChildren: () => import('../modules/dashboard/dashboard.module').then(m => m.DashboardModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
