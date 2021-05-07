import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable } from 'rxjs';

import { ATBaseService } from '../../at-base.service';
import { AppSettingService } from '../../../configs/app-setting.service';
import { SessionTokenService } from '../../../stores/authentication/session-token.service';
import { ODataQueryParameters, TeamMemberModel, TeamMemberUpdateModel, TeamMemberAddModel, TeamMemberOrientationUpdateModel } from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class TeamMemberService extends ATBaseService {

  private dataUrl: string = `${this.appSettingService.apiUrl}/teammember`;

  constructor(
    public http: HttpClient,
    public appSettingService: AppSettingService ,
    public sessionTokenService: SessionTokenService
  )
  {
    super(http,appSettingService,sessionTokenService);
  }

  getTeamMember(teamMemberId : number, oDataParams?: ODataQueryParameters) : Observable<TeamMemberModel>{
     return this.getAction<TeamMemberModel>(`${this.dataUrl}/${teamMemberId}`, oDataParams);
  }

  getTeamMembers(oDataParams?: ODataQueryParameters) : Observable<TeamMemberModel[]>{
    return this.getAction<TeamMemberModel[]>(this.dataUrl , oDataParams);
  }

  getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
    return this.getCountAction<number>(this.dataUrl , oDataParams);
  }

  addTeamMember(data : TeamMemberAddModel, oDataParams?: ODataQueryParameters): Observable<TeamMemberModel>{
    return this.addAction<TeamMemberModel>(this.dataUrl, data, oDataParams);
  }

  updateTeamMember(teamMemberId : number, data : TeamMemberUpdateModel, oDataParams?: ODataQueryParameters): Observable<TeamMemberModel>{
    return this.updateAction<TeamMemberModel>(`${this.dataUrl}/${teamMemberId}`, data, oDataParams);
  }

  deleteTeamMember(teamMemberId : number): Observable<boolean>{
    return this.deleteAction<TeamMemberModel>(`${this.dataUrl}/${teamMemberId}`);
  }

  updateTeamMemberPosition(models : TeamMemberOrientationUpdateModel[]): Observable<boolean>{
    return this.updateAction(`${this.dataUrl}/updateposition`, models);
  }
}
