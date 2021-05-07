import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfilesRoutingModule } from './profiles-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { ProfilesLayoutComponent } from './profiles-layout.component';
import { ProfileGeneralComponent } from './views/profile-general/profile-general.component';
import { ProfileEditComponent } from './views/profile-general/elements/profile-edit/profile-edit..component';
import { ProfileChangePasswordComponent } from './views/profile-general/elements/profile-change-password/profile-change-password.component';
import { ProfileAuthTrackComponent } from './views/profile-auth-track/profile-auth-track.component';


@NgModule({
  declarations: [ProfilesLayoutComponent, ProfileGeneralComponent, ProfileEditComponent, ProfileChangePasswordComponent,ProfileAuthTrackComponent],
  imports: [
    CommonModule,
    ProfilesRoutingModule,
    SharedModule
  ]
})
export class ProfilesModule { }
