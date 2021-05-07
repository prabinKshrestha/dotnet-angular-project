import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'atLocalTime'
})

export class ATLocalDateTimePipe extends DatePipe implements PipeTransform {

  transform(value: Date, customFormat? : "detail" | "basic"): string {
    if(value){
      let date : Date = new Date(value+'z')
      switch(customFormat){
        case "detail":
          return super.transform(date, "hh:mm aa, EEEE, dd MMM yyyy");
        case "basic":
        default:     
          return super.transform(date, "dd MMM, yyyy");
      }
    }
  }

}
