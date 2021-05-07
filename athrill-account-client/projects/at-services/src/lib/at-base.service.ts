import { Injectable } from '@angular/core';

import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError} from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { ODataQueryParameters, 
          ResponseHeaderPropertiesConstants, 
          UriODataQueryConstants,
          ATAppContextConstants, 
          FormDataBaseAddModel, 
          FormDataBaseUpdateModel,
          ATBusinessExceptionModel, 
          FormDataBaseFormModel} from 'at-models';
        
import {AppSettingService} from '../configs/app-setting.service';
import { SessionTokenService } from '../stores/authentication/session-token.service';

@Injectable({
  providedIn: 'root'
})

export abstract class ATBaseService {

  constructor(
    public httpClient: HttpClient,
    public appSettingService: AppSettingService,
    public sessionTokenService: SessionTokenService
  ) { }

  protected getAction<T>(urlPath : string, oDataParams? : ODataQueryParameters, params?:{ }) : Observable<T>
  {
    return this.httpClient.get<T>(this.constructRequestUrl(urlPath,oDataParams,params), {headers : this.constructRequestHeaders(), observe: "response"})
                .pipe(
                    map((response : HttpResponse<T>) => {
                        this.updateSessionToken();
                        return response.body as T;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected headAction<T>(urlPath : string, oDataParams? : ODataQueryParameters, params?:{ }) : Observable<HttpHeaders>
  {
    return this.httpClient.head<T>(this.constructRequestUrl(urlPath,oDataParams,params), {headers : this.constructRequestHeaders(), observe: "response"})
                .pipe(
                    map((response : HttpResponse<T>) => {
                      this.updateSessionToken();
                        return response.headers;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected addAction<T>(urlPath : string, data :any, oDataParams? : ODataQueryParameters, params?:{ }) : Observable<T>
  {
    this.trimStringData(data);
    if(data instanceof FormDataBaseAddModel || data instanceof FormDataBaseFormModel){
      data = this.convertToFormData(data);
    }
    return this.httpClient.post<T>(this.constructRequestUrl(urlPath,oDataParams,params),
                                   data, 
                                  {headers : this.constructRequestHeaders(), observe: "response"}
                ).pipe(
                    map((response : HttpResponse<T>) => {
                      this.updateSessionToken();
                        return response.body;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected updateAction<T>(urlPath : string, data :any, oDataParams? : ODataQueryParameters, params?:{ }) : Observable<T>
  {
    this.trimStringData(data);
    if(data instanceof FormDataBaseUpdateModel || data instanceof FormDataBaseFormModel){
      data = this.convertToFormData(data);
    }
    return this.httpClient.put<T>(this.constructRequestUrl(urlPath,oDataParams,params), data,  {headers : this.constructRequestHeaders(), observe: "response"})
                .pipe(
                    map((response : HttpResponse<T>) => {
                      this.updateSessionToken();
                        return response.body;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected deleteAction<T>(urlPath : string, params?:{}) : Observable<boolean>
  {
    return this.httpClient.delete<T>(this.constructRequestUrl(urlPath,null,params),  {headers : this.constructRequestHeaders(), observe: "response"})
                .pipe(
                    map((response : HttpResponse<T>) => {
                      this.updateSessionToken();
                        return response.ok;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected getCountAction<T>(urlPath : string, oDataParams? : ODataQueryParameters, params?:{ }) : Observable<number>
  {
    return this.httpClient.head<T>(this.constructRequestUrl(urlPath,oDataParams,params), {headers : this.constructRequestHeaders(), observe: "response"})
                .pipe(
                    map((response : HttpResponse<T>) => {
                      this.updateSessionToken();
                        return response.headers.get(ResponseHeaderPropertiesConstants.COUNT);
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected downloadAction(urlPath : string) : Observable<Blob>
  {
    return this.httpClient.get(this.constructRequestUrl(urlPath), { headers : this.constructRequestHeaders(), responseType: 'blob' })
                .pipe(
                    map(res => {
                        this.updateSessionToken();
                        return res;
                    }),
                    catchError((error : any) => this.handleError(error))
                );
  }

  protected constructRequestHeaders() : HttpHeaders{
    if(this.appSettingService.appContext === ATAppContextConstants.AT_SUPPORT_SITE){
      return new HttpHeaders({
        'x-app-context': this.appSettingService.appContext,
        'Authorization' : `Bearer ${this.sessionTokenService.getSessionToken()}`
      });
    }else{
      return new HttpHeaders({
        'x-app-context': this.appSettingService.appContext
      });
    }                                                       
  }

  protected constructRequestUrl(apiUrl: string, oDataParams? : ODataQueryParameters, params?: {}) : string
  {
     let path = apiUrl;
     path += "?";
     if(params){
       for(let prop in params){
          path += path.endsWith("?") || path.endsWith("&") ? "" : "&"; 
          path += `${prop}=${encodeURIComponent(params[prop])}`;
       }
     }
     if(oDataParams){
      path = this.getODataQueryParamsSet(path, oDataParams);
     }
     return path.endsWith("?") || path.endsWith("&") ? path.slice(0,-1): path;
  }

  private getODataQueryParamsSet(path : string, oDataParams: ODataQueryParameters) : string 
  {
    Object.keys(oDataParams).forEach(key => {
      path += path.endsWith("?") || path.endsWith("&") ? "" : "&"; 
      switch(key){
        case "Expand":
          if(oDataParams.Expand){
            path += `${UriODataQueryConstants.EXPAND}=${encodeURIComponent(oDataParams.Expand)}`;
          }
          break;
        case "Select":
          if(oDataParams.Select){
            path += `${UriODataQueryConstants.SELECT}=${encodeURIComponent(oDataParams.Select)}`;
          }
          break;
        case "Search":
          if(oDataParams.Search){}
          path += `${UriODataQueryConstants.SEARCH}=${encodeURIComponent(oDataParams.Search)}`;
          break;
        case "Skip":
            path += `${UriODataQueryConstants.SKIP}=${encodeURIComponent(oDataParams.Skip)}`;
          break;
        case "Top":
            path += `${UriODataQueryConstants.TOP}=${encodeURIComponent(oDataParams.Top)}`;
          break;
        case "Nopaging":
            path += `${UriODataQueryConstants.NOPAGING}=${encodeURIComponent(oDataParams.Nopaging)}`;
          break;
        case "Orderby":
          if(oDataParams.Orderby){
            path += `${UriODataQueryConstants.ORDERBY}=${encodeURIComponent(oDataParams.Orderby)}`;
          }
          break;
        case "Filter":
          if(oDataParams.Filter){
            path += `${UriODataQueryConstants.FILTER}=${encodeURIComponent(oDataParams.Filter)}`;
          }
          break;
        default : 
          path += "";
          break;
      }
    });
    return path;
  }

  public convertToFormData(data: any): any
  {
    let formData = new FormData();
    Object.keys(data).forEach(key => {
      if(data[key])
      {
        if(Array.isArray(data[key])){
          data[key].forEach((element, index) => {
            if(typeof element == 'object' && !(element instanceof File)){
              element = JSON.stringify(element);
            }
            formData.append(`${key}`, element);
          });
        }else{
          if(typeof data[key] == 'object' && !(data[key] instanceof File)){
            data[key] = JSON.stringify(data[key]);
          }
            formData.append(key, data[key]);
        }
      }
    });
    return formData;
  }
  
  private trimStringData(data : any){
    // this method is necessary so that unnecessary leading and trailing spaces wont be in values.
    // if(data != null && typeof(data) === "object"){
    //     Object.keys(data).forEach(key => {
    //       if(typeof(data) === "string"){
    //         data[key] = data[key].trim();
    //       }else if(data[key] != null  && typeof(data[key] === "object")){
    //         this.trimStringData(data);
    //       }
    //     });
    // }
  }

  protected handleError(error: any): Observable<any>{
    if(error){
      let validationExceptions : ATBusinessExceptionModel = new ATBusinessExceptionModel;
      if(error.status){
        switch (error.status)
        {
          case 400:
              let httpError = error.error;
              validationExceptions.Validations = httpError.Validations;
              validationExceptions.Message = httpError.Message;
              return throwError(validationExceptions);

          case 401: // unauthorized error
            validationExceptions.Message = "You are not Unauthorized";
            this.sessionTokenService.invalidateSession();
            window.location.href = location.href;
            return throwError(validationExceptions);

          case 402:
              validationExceptions.Message = error.error.Message;
              return throwError(validationExceptions);

          case 403:
              validationExceptions.Message = error.error.Message;
              validationExceptions.Validations = error.error.Validations;
              return throwError(validationExceptions);

          case 500:
              validationExceptions.Message = "Internal Server Error. Please contact administrator if error persist.";
              return throwError(validationExceptions);

          default:
            break;
        }
        return throwError(error);
      }
      return throwError(error);
    }
    return throwError("Server Error. Specific Error Not Found.");
  }

  protected updateSessionToken(): void
  {
    if(this.appSettingService.appContext === ATAppContextConstants.AT_SUPPORT_SITE)
    {
      this.sessionTokenService.updateSessionToken();
    }
  }
}
