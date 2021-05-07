import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ODataQueryParameters, ATEntityModel } from 'at-models';
import { ATBaseService } from '../../at-base.service';
import { AppSettingService } from '../../../configs/app-setting.service';
import { SessionTokenService } from '../../../stores/authentication/session-token.service';

@Injectable({
    providedIn: 'root'
  })

  export class ATEntityService extends ATBaseService {

    private dataUrl: string = `${this.appSettingService.apiUrl}/entity`;
  
    constructor(
      public http: HttpClient,
      public appSettingService: AppSettingService ,
      public sessionTokenService: SessionTokenService
    ) 
    { 
      super(http,appSettingService,sessionTokenService);
    }
  
    getATEntity(atEntityId : number, oDataParams?: ODataQueryParameters) : Observable<ATEntityModel>{
       return this.getAction<ATEntityModel>(`${this.dataUrl}/${atEntityId}`, oDataParams);
    }
  
    getATEntities(oDataParams?: ODataQueryParameters) : Observable<ATEntityModel[]>{
      return this.getAction<ATEntityModel[]>(this.dataUrl , oDataParams);
    }
  
    getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
      return this.getCountAction<number>(this.dataUrl , oDataParams);
    }
  }