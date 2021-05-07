import { Injectable } from '@angular/core';
import { ATStoreService } from '../at-store.service';
import {ATStorageKeyConstants} from 'at-models';
import { AppSettingService } from '../../configs/app-setting.service';
import { UserContextService } from './user-context.service';
import { UserPermissionService } from './user-permission.service';

@Injectable({
  providedIn: 'root'
})
export class SessionTokenService extends ATStoreService{

  private sessionTokenKey : string = ATStorageKeyConstants.SESSION_TOKEN;
  private tokenExpireTTL : string = ATStorageKeyConstants.TOKEN_EXPIRE_TTL;

  constructor(
    private _appSettingService : AppSettingService,
    private _userContextService : UserContextService,
    private _userPermissionService : UserPermissionService
  ) 
  {
    super();
    this.STORAGE_KEY = this.sessionTokenKey;
  }

  public setSession(sessionToken : string, expiresTTL : Date) : void
  {
    this.setToStore(sessionToken);
    this.setToStoreWithArg(expiresTTL, this.tokenExpireTTL);
  }
  
  public invalidateSession() : void{
      this.removeFromStore();
      this.removeFromStoreWithArg(this.tokenExpireTTL);
      this._userContextService.clear();
      this._userPermissionService.clear();
      this._userContextService.userContext.next(null);
  }

  public getSessionToken() : string{
    return this.getFromStore<string>();
  }

  public isLoggedIn() : boolean
  {
    return this.hasKeyInStore() && 
           this.hasKeyInStoreWithArg(this.tokenExpireTTL) && 
           new Date(this.getFromStoreWithArg<Date>(this.tokenExpireTTL)) > new Date(); 
  }

  public updateSessionToken() : void
  {
    // write update token code here
    if(this.hasKeyInStoreWithArg(this.tokenExpireTTL)){
      let nowDate : Date = new Date();
      nowDate.setHours(nowDate.getHours() + this._appSettingService.sessionTokenTTL);
      this.setToStoreWithArg(nowDate, this.tokenExpireTTL);
    }
  }
}
