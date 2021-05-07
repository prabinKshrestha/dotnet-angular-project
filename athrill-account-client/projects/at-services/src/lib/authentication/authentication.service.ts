import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders ,HttpErrorResponse} from '@angular/common/http';
import { Observable} from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ok } from 'assert';

import {LoginModel,AuthenticationResponseModel, ResetPasswordModel, UserModel, UserRegistrationModel, ChangePasswordModel} from 'at-models';
import { ATBaseService } from '../at-base.service';
import {AppSettingService} from '../../configs/app-setting.service';
import { SessionTokenService } from '../../stores/authentication/session-token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends ATBaseService{

  private dataUrl = `${this.appSettingService.apiUrl}/authentication`;

  constructor(
    public httpClient: HttpClient,
    public appSettingService: AppSettingService,
    public sessionTokenService: SessionTokenService
  )
  { 
    super(httpClient,appSettingService,sessionTokenService);
  }

  public signIn(loginModel : LoginModel): Observable<AuthenticationResponseModel>
  {
    return this.signInApiCall(loginModel);
  }

  public signOut(): Observable<any>
  {
    return this.signOutApiCall();
  }

  public registerUser(registerUserModel : UserRegistrationModel): Observable<UserModel>
  {
    return this.addAction<UserModel>(`${this.dataUrl}/register`,registerUserModel);
  }

  public changePassword(model : ChangePasswordModel): Observable<UserModel>
  {
    return this.updateAction(`${this.dataUrl}/changepassword`,model);
  }

  public resetPassword(resetPasswordModel : ResetPasswordModel): Observable<any>
  {
    return this.httpClient.post<AuthenticationResponseModel>(this.dataUrl + '/resetpassword', resetPasswordModel, {headers : this.getHeadersForRequest(), observe: "response"})
            .pipe(map((response : HttpResponse<any>) => {
                    return response.ok
                }),catchError((error : any) => this.handleError(error))
            );
  }

  private signInApiCall(loginModel: LoginModel): Observable<AuthenticationResponseModel>{
    return this.httpClient.post<AuthenticationResponseModel>(this.dataUrl + '/login', loginModel, {headers : this.getHeadersForRequest(), observe: "response"})
                  .pipe(map((response : HttpResponse<AuthenticationResponseModel>) => {
                          return response.body as AuthenticationResponseModel;
                      }),catchError((error : any) => this.handleError(error))
                  )
  }

  private signOutApiCall(): Observable<any>{
       return this.httpClient.post(this.dataUrl + '/logout', {} , {headers : this.getHeadersWithToken(), observe: "response"});
  }

  private getHeadersForRequest(): HttpHeaders
  {
    return new HttpHeaders({
      'content-type': "application/json",
      'x-app-context': this.appSettingService.appContext
    });
  }
  private getHeadersWithToken(): HttpHeaders
  {
    return new HttpHeaders({
      'content-type': "application/json",
      'x-app-context': this.appSettingService.appContext,
      'Authorization' : `Bearer ${this.sessionTokenService.getSessionToken()}`
    });
  }
}
