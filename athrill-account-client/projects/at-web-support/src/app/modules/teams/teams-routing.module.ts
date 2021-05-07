import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TeamsLayoutComponent } from './teams-layout.component';
import { TeamCategoryOrientationComponent } from './views/team-category-orientation/team-category-orientation.component';
import { TeamCategoryComponent } from './views/team-category/team-category.component';
import { TeamMemberOrientationComponent } from './views/team-member-orientation/team-member-orientation.component';
import { TeamMemberComponent } from './views/team-member/team-member.component';


const routes: Routes = [
    {
        path: '',
        component: TeamsLayoutComponent,
        children : [
          {
            path: 'category',
            component: TeamCategoryComponent,
            pathMatch: 'full'
          },
          {
            path: 'category/orientation',
            component: TeamCategoryOrientationComponent,
            pathMatch: 'full'
          },
          {
            path: 'member',
            component: TeamMemberComponent,
            pathMatch: 'full'
          },
          {
            path: 'member/orientation',
            component: TeamMemberOrientationComponent,
            pathMatch: 'full'
          },
          {
            path: '',
            redirectTo: 'category'
          },
          {
            path: '**',
            component: TeamCategoryComponent
          }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule { }
