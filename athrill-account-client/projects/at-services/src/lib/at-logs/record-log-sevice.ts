import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ATBaseService } from '../at-base.service';
import { AppSettingService } from '../../configs/app-setting.service';
import { SessionTokenService } from '../../stores/authentication/session-token.service';
import { ODataQueryParameters, RecordLogModel } from 'at-models';

@Injectable({
    providedIn: 'root'
  })
  
export class RecordLogService extends ATBaseService {

    private dataUrl: string = `${this.appSettingService.apiUrl}/recordlog`;
  
    constructor(
      public http: HttpClient,
      public appSettingService: AppSettingService ,
      public sessionTokenService: SessionTokenService
    ) 
    { 
      super(http,appSettingService,sessionTokenService);
    }
  
    getRecordLogs(oDataParams?: ODataQueryParameters) : Observable<RecordLogModel[]>{
      return this.getAction<RecordLogModel[]>(this.dataUrl , oDataParams);
    }

    getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
      return this.getCountAction<number>(this.dataUrl , oDataParams);
    }
    
    getLastRecordLog(recordLogId : number, oDataParams?: ODataQueryParameters) : Observable<RecordLogModel>{
      return this.getAction<RecordLogModel>(`${this.dataUrl}/${recordLogId}/getlastrecordlog` , oDataParams);
    }
    
  }