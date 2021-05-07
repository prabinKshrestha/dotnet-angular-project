import { Pipe, PipeTransform } from '@angular/core';
import { AppSettingService } from 'at-services';

@Pipe({
  name: 'atNetworkImage'
})
export class ATNetworkImagePipe implements PipeTransform {

  constructor(
    private _appSettingService : AppSettingService
  ){}

  transform(imageUrlFromServer: string): string {
    if(imageUrlFromServer){
      return this._appSettingService.apiUrl + '/' + imageUrlFromServer;
    }
    return;
  }

}
