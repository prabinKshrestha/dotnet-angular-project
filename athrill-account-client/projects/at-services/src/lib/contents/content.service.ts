import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable } from 'rxjs';

import { ATBaseService } from '../at-base.service';
import { AppSettingService } from '../../configs/app-setting.service';
import { SessionTokenService } from '../../stores/authentication/session-token.service';
import { ODataQueryParameters, ContentModel, ContentUpdateModel, ContentAddModel, ContentTypeModel, ContentTreeModel } from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class ContentService extends ATBaseService {

  private dataUrl: string = `${this.appSettingService.apiUrl}/content`;

  constructor(
    public http: HttpClient,
    public appSettingService: AppSettingService ,
    public sessionTokenService: SessionTokenService
  ) 
  { 
    super(http,appSettingService,sessionTokenService);
  }

  getContent(contentId : number, oDataParams?: ODataQueryParameters) : Observable<ContentModel>{
     return this.getAction<ContentModel>(`${this.dataUrl}/${contentId}`, oDataParams);
  }

  getContents(oDataParams?: ODataQueryParameters) : Observable<ContentModel[]>{
    return this.getAction<ContentModel[]>(this.dataUrl , oDataParams);
  }

  getCounts(oDataParams?: ODataQueryParameters) : Observable<number>{
    return this.getCountAction<number>(this.dataUrl , oDataParams);
  }

  addContent(data : ContentAddModel, oDataParams?: ODataQueryParameters): Observable<ContentModel>{
    return this.addAction<ContentModel>(this.dataUrl, data, oDataParams);
  }
  
  updateContent(contentId : number, data : ContentUpdateModel, oDataParams?: ODataQueryParameters): Observable<ContentModel>{
    return this.updateAction<ContentModel>(`${this.dataUrl}/${contentId}`, data, oDataParams);
  }

  getContentTrees(): Observable<ContentTreeModel[]>{
    return this.getAction<ContentTreeModel[]>(`${this.dataUrl}/tree`);
  }
  
  updateContentTrees(data : ContentTreeModel[]): Observable<any>{
    return this.updateAction(`${this.dataUrl}/tree`, data);
  }

  
  deleteContent(contentId : number): Observable<boolean>{
    return this.deleteAction<ContentModel>(`${this.dataUrl}/${contentId}`);
  }

  getAllContentTypes() : Observable<ContentTypeModel[]>{
    return this.getAction<ContentTypeModel[]>(`${this.dataUrl}/contenttypes`);
  }
}