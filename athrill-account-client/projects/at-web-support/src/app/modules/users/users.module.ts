import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersLayoutComponent } from './users-layout.component';
import { SharedModule } from '../../shared/shared.module';
import { UserGeneralComponent } from './views/user-general/user-general.component';
import { UserRegistrationComponent } from './views/user-general/elements/user-registration/user-registration.component';
import { UserDetailComponent } from './views/user-general/elements/user-detail/user-detail.component';
import { UserAuthTrackComponent } from './views/user-auth-track/user-auth-track.component';
import { UserEditComponent } from './views/user-general/elements/user-edit/user-edit.component';

@NgModule({
  declarations: [UsersLayoutComponent, UserGeneralComponent, UserRegistrationComponent, UserDetailComponent, UserAuthTrackComponent, UserEditComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule
  ]
})
export class UsersModule { }
