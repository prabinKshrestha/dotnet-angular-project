import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ATAppContextConstants, SiteSettingModel } from 'at-models';
import { AppSettingService, SiteSettingService } from 'at-services';
import { environment } from '../environments/environment';
import { ATGlobalStore } from './shared/services/at-global.store';

@Component({
  selector: 'at-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(
    private _appSettingService: AppSettingService,
    private _siteSettingService: SiteSettingService,
    private _titleService: Title,
    private _atGlobalStore: ATGlobalStore
  ) {
    this.setAppSettings();
  }

  ngOnInit() {
    this.setHeadTags();
    this.setSiteSettings();
  }

  private setHeadTags(): void {
    this._atGlobalStore.siteSettingSubject.subscribe(siteSetting => {
      this._titleService.setTitle(siteSetting?.MetaTitle ? `${siteSetting?.MetaTitle} | Support Site` : 'Support Site');
    });
  }

  private setSiteSettings(): void {
    this._siteSettingService.getSiteSetting().subscribe(x => {
      this._atGlobalStore.siteSetting = x;
    });
  }

  private setAppSettings(): void {
    this._appSettingService.developerTeamName = environment.developerTeamName;
    this._appSettingService.developerTeamWebsiteUrl = environment.developerTeamWebsiteUrl;
    this._appSettingService.apiUrl = environment.apiUrl;
    this._appSettingService.sessionTokenTTL = environment.sessionTokenTTL;
    this._appSettingService.appContext = ATAppContextConstants.AT_SUPPORT_SITE;
    // this._appSettingService.isUserTrackRecordsEnabled = environment.isUserTrackRecordsEnabled;
    // this._appSettingService.isRecordLogsEnabled = environment.isRecordLogsEnabled;
    // this._appSettingService.isBarCodeFeatureEnabled = environment.isBarCodeFeatureEnabled;
  }

}
