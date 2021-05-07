import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

import { ATBaseService } from '../../at-base.service';
import { AppSettingService } from '../../../configs/app-setting.service';
import { SessionTokenService } from '../../../stores/authentication/session-token.service';
import { ODataQueryParameters, SiteSettingModel, SiteSettingUpdateModel } from 'at-models';

@Injectable({
  providedIn: 'root'
})
export class SiteSettingService extends ATBaseService{

  private dataUrl: string = `${this.appSettingService.apiUrl}/sitesetting`;

  public siteSetting : BehaviorSubject<SiteSettingModel> = new BehaviorSubject<SiteSettingModel>(null);

  constructor(
    public httpClient: HttpClient,
    public appSettingService: AppSettingService,
    public sessionTokenService: SessionTokenService
  ) 
  { 
     super(httpClient, appSettingService,sessionTokenService);
  }

  getSiteSetting() : Observable<SiteSettingModel>{
     return this.getAction<SiteSettingModel>(`${this.appSettingService.apiUrl}/sitesetting`);
  }
  
  updateSiteSetting(data : SiteSettingUpdateModel): Observable<SiteSettingModel>{
    return this.updateAction<SiteSettingModel>(`${this.appSettingService.apiUrl}/sitesetting`, data);
  }
}

