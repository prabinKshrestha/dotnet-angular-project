import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable } from 'rxjs';

import { ATBaseService } from '../../at-base.service';
import { AppSettingService } from '../../../configs/app-setting.service';
import { SessionTokenService } from '../../../stores/authentication/session-token.service';
import { ODataQueryParameters, TeamCategoryModel, TeamCategoryUpdateModel, TeamCategoryAddModel } from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class TeamCategoryService extends ATBaseService {

  private dataUrl: string = `${this.appSettingService.apiUrl}/teamcategory`;

  constructor(
    public http: HttpClient,
    public appSettingService: AppSettingService ,
    public sessionTokenService: SessionTokenService
  ) 
  { 
    super(http,appSettingService,sessionTokenService);
  }

  getTeamCategory(teamCategoryId : number, oDataParams?: ODataQueryParameters) : Observable<TeamCategoryModel>{
     return this.getAction<TeamCategoryModel>(`${this.dataUrl}/${teamCategoryId}`, oDataParams);
  }

  getTeamCategories(oDataParams?: ODataQueryParameters) : Observable<TeamCategoryModel[]>{
    return this.getAction<TeamCategoryModel[]>(this.dataUrl , oDataParams);
  }

  getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
    return this.getCountAction<number>(this.dataUrl , oDataParams);
  }

  addTeamCategory(data : TeamCategoryAddModel, oDataParams?: ODataQueryParameters): Observable<TeamCategoryModel>{
    return this.addAction<TeamCategoryModel>(this.dataUrl, data, oDataParams);
  }
  
  updateTeamCategory(teamCategoryId : number, data : TeamCategoryUpdateModel, oDataParams?: ODataQueryParameters): Observable<TeamCategoryModel>{
    return this.updateAction<TeamCategoryModel>(`${this.dataUrl}/${teamCategoryId}`, data, oDataParams);
  }
  
  deleteTeamCategory(teamCategoryId : number): Observable<boolean>{
    return this.deleteAction<TeamCategoryModel>(`${this.dataUrl}/${teamCategoryId}`);
  }
  
  updateTeamCategoryPosition(teamCategoryIds : number[]): Observable<boolean>{
    return this.updateAction(`${this.dataUrl}/updateposition`, teamCategoryIds);
  }
}
