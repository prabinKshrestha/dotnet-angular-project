import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { TeamsLayoutComponent } from './teams-layout.component';
import { TeamCategoryComponent } from './views/team-category/team-category.component';
import { TeamCategoryDetailComponent } from './views/team-category/elements/team-category-detail/team-category-detail.component';
import { TeamCategoryFormComponent } from './views/team-category/elements/team-category-form/team-category-form.component';
import { TeamMemberComponent } from './views/team-member/team-member.component';
import { TeamMemberFormComponent } from './views/team-member/elements/team-member-form/team-member-form.component';
import { TeamMemberDetailComponent } from './views/team-member/elements/team-member-detail/team-member-detail.component';
import { TeamCategoryOrientationComponent } from './views/team-category-orientation/team-category-orientation.component';
import { TeamMemberOrientationComponent } from './views/team-member-orientation/team-member-orientation.component';


@NgModule({
  declarations: [
    TeamsLayoutComponent, 
    TeamCategoryComponent, 
    TeamCategoryDetailComponent, 
    TeamCategoryFormComponent,
    TeamMemberComponent,
    TeamMemberFormComponent,
    TeamMemberDetailComponent,
    TeamCategoryOrientationComponent,
    TeamMemberOrientationComponent
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    SharedModule
  ]
})
export class TeamsModule { }
