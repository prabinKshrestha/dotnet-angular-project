import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

import { AppSettingService } from '../../../configs/app-setting.service';
import { ATBaseService } from '../../at-base.service';
import { SessionTokenService } from '../../../stores/authentication/session-token.service';
import { EmailSettingModel, EmailSettingAddModel, EmailSettingUpdateModel, ODataQueryParameters } from 'at-models';

@Injectable({
  providedIn: 'root'
})
export class EmailSettingService extends ATBaseService{

  private dataUrl: string = `${this.appSettingService.apiUrl}/emailsetting`;

  constructor(
    public httpClient: HttpClient,
    public appSettingService: AppSettingService,
    public sessionTokenService: SessionTokenService
  ) 
  { 
     super(httpClient, appSettingService,sessionTokenService);
  }
  
  getEmailSetting(emailSettingId : number, oDataParams?: ODataQueryParameters) : Observable<EmailSettingModel>{
    return this.getAction<EmailSettingModel>(`${this.dataUrl}/${emailSettingId}`, oDataParams);
 }

 getEmailSettings(oDataParams?: ODataQueryParameters) : Observable<EmailSettingModel[]>{
   return this.getAction<EmailSettingModel[]>(this.dataUrl, oDataParams);
 }

 getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
   return this.getCountAction<number>(this.dataUrl, oDataParams);
 }

 addEmailSetting(data : EmailSettingAddModel, oDataParams?: ODataQueryParameters): Observable<EmailSettingModel>{
   return this.addAction<EmailSettingModel>(this.dataUrl, data, oDataParams);
 }
 
 updateEmailSetting(emailSettingId : number, data : EmailSettingUpdateModel, oDataParams?: ODataQueryParameters): Observable<EmailSettingModel>{
   return this.updateAction<EmailSettingModel>(`${this.dataUrl}/${emailSettingId}`, data, oDataParams);
 }
 
 deleteEmailSetting(emailSettingId : number): Observable<boolean>{
   return this.deleteAction<EmailSettingModel>(`${this.dataUrl}/${emailSettingId}`);
 }

 changeDefaultStatus(emailSettingId : number, changedDefaultStatus : boolean): Observable<any>{
   return this.updateAction(`${this.dataUrl}/${emailSettingId}/changedefaultstatus/${changedDefaultStatus}`, null);
  }
}
