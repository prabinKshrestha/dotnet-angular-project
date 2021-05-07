import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppSettingService {

  public developerTeamName : string = "Athirll Technology";
  public developerTeamWebsiteUrl : string = "https://www.techathrill.com";

  public appContext : string = "Unknown";
  public apiUrl : string = "https://localhost:44340";
  public sessionTokenTTL: number = 0;

  public isUserTrackRecordsEnabled : boolean = false;
  public isRecordLogsEnabled : boolean = false;
  public isBarCodeFeatureEnabled : boolean = false;

  constructor() { }
}
