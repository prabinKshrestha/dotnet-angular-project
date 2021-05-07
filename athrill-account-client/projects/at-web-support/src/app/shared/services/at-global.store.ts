import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SiteSettingModel, UserModel } from 'at-models';

@Injectable({
    providedIn: 'root'
})

export class ATGlobalStore {

    public siteSettingSubject : BehaviorSubject<SiteSettingModel> = new BehaviorSubject<SiteSettingModel>(null);
    private _siteSetting : SiteSettingModel;

    public userInformationSubject : BehaviorSubject<UserModel> = new BehaviorSubject<UserModel>(null);
    private _userInformation : UserModel;

    set siteSetting(value : SiteSettingModel){
        this._siteSetting = value;
        this.siteSettingSubject.next(value);
    }

    get siteSetting(){
        return this._siteSetting;
    }

    set userInformation(value : UserModel){
        this._userInformation = value;
        this.userInformationSubject.next(value);
    }

    get userInformation(){
        return this._userInformation;
    }

}