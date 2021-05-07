import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable } from 'rxjs';

import { ATBaseService } from '../at-base.service';
import { AppSettingService } from '../../configs/app-setting.service';
import { SessionTokenService } from '../../stores/authentication/session-token.service';
import { ODataQueryParameters, UserModel, UserRoleModel ,UserUpdateModel, UserTrackRecordModel, UserEditModel} from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class UserService extends ATBaseService {

  private dataUrl: string = `${this.appSettingService.apiUrl}/user`;

  constructor(
    public http: HttpClient,
    public appSettingService: AppSettingService ,
    public sessionTokenService: SessionTokenService
  ) 
  { 
    super(http,appSettingService,sessionTokenService);
  }

  getUser(userId : number, oDataParams?: ODataQueryParameters) : Observable<UserModel>{
     return this.getAction<UserModel>(`${this.dataUrl}/${userId}`, oDataParams);
  }

  getUsers(oDataParams?: ODataQueryParameters) : Observable<UserModel[]>{
    return this.getAction<UserModel[]>(this.dataUrl , oDataParams);
  }

  getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
    return this.getCountAction<number>(this.dataUrl , oDataParams);
  }
  
  deleteUser(userId : number): Observable<boolean>{
    return this.deleteAction<UserModel>(`${this.dataUrl}/${userId}`);
  }
  
  updateUser(userId : number, data : UserUpdateModel, oDataParams?: ODataQueryParameters): Observable<UserModel>{
    return this.updateAction<UserModel>(`${this.dataUrl}/${userId}`, data, oDataParams);
  }
  
  changeActiveStatus(userId : number, changeActiveStatus : boolean): Observable<any>{
    return this.updateAction(`${this.dataUrl}/${userId}/changeactivestatus/${changeActiveStatus}`, null);
  }
  
  getUserRoles(): Observable<any>{
    return this.getAction<UserRoleModel[]>(`${this.dataUrl}/roles`);
  }
  
  getUserTrackRecords(oDataParams?: ODataQueryParameters): Observable<UserTrackRecordModel[]>{
    return this.getAction<UserTrackRecordModel[]>(`${this.dataUrl}/usertrackrecords`, oDataParams);
  }

  getCountsForUserTrackRecords(oDataParams?: ODataQueryParameters): Observable<number>{
    return this.getCountAction<number[]>(`${this.dataUrl}/usertrackrecords`, oDataParams);
  }

  updateRegisteredUser(userId : number, data : UserEditModel): Observable<any>{
    return this.updateAction<any>(`${this.dataUrl}/${userId}/updateregistereduser`, data);
  }
}