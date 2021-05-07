import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable } from 'rxjs';

import { ATBaseService } from '../at-base.service';
import { AppSettingService } from '../../configs/app-setting.service';
import { SessionTokenService } from '../../stores/authentication/session-token.service';
import { ATDataValueModel, ATDataTypes } from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class ATDatasService extends ATBaseService {

  private dataUrl: string = `${this.appSettingService.apiUrl}/atdatas`;

  constructor(
    public http: HttpClient,
    public appSettingService: AppSettingService ,
    public sessionTokenService: SessionTokenService
  ) 
  { 
    super(http,appSettingService,sessionTokenService);
  }

  getATDataValuesByType(aTDataType : ATDataTypes) : Observable<ATDataValueModel[]>{
     return this.getAction<ATDataValueModel[]>(`${this.dataUrl}/getatdatavaluesbytype/${aTDataType}`);
  }
}