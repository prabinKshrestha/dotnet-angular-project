import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageHelperService {

  constructor() { }

  public getImageForFileTypes(extension : string): string{
    let retVal: string = 'assets/file-types/';
    if(extension){
      extension = extension.trim();
      if(extension.startsWith('.')){
        extension = extension.substring(1);
      }
      switch(extension){
        case 'pdf':
          retVal += 'pdf.png';
          break;
        case 'doc':
          retVal += 'doc.png';
          break;
        case 'docx':
          retVal += 'docx.png';
          break;
        case 'txt':
          retVal += 'txt.png';
          break;
        default:
          retVal += 'common.png';
      }
    }
    return retVal;
  }

}
