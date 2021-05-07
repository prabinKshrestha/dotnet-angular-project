import { Injectable } from '@angular/core';
import { ATBusinessExceptionModel } from 'at-models';

@Injectable({
  providedIn: 'root'
})

export class CommonHelperService {

  constructor() { }

  public clone(source : any) : any
  {
    return JSON.parse(JSON.stringify(source));
  }

  public parseError(error : ATBusinessExceptionModel) : string
  {
    let message: string = error.Message;
    if (error.Validations && error.Validations.length > 0) {
        message += `<br>`;
        error.Validations.forEach(e => message += `<br>${e.Message}`);
    }
    return message;
  }

  public Map<T>(source : any, destination : T) : T
  {
    Object.assign(destination, source);
    return destination;
  }

}
