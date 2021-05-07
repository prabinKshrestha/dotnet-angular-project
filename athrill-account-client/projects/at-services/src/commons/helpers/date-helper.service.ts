import { Injectable } from '@angular/core';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})

export class DateHelperService {

  constructor() { }

  public convertToISOString(date: Date): string {
    return moment(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString();
  }

  public convertToISOStringWithOutZ(date: Date): string {
    return moment(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString().replace('Z', '');
  }

  public convertToUTCDate(date: string, replaceZ?: boolean): string {
    let retVal: string = moment(date).utc().format();
    if (replaceZ) {
      retVal = retVal.replace('Z', '')
    }
    return retVal;
  }

}
