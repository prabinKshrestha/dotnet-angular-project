import { ChangeDetectorRef, Component } from '@angular/core';
import { ATBusinessExceptionMessageModel, ATBusinessExceptionModel, ATErrorLevel } from 'at-models';
import { ATFormNotificationService } from './at-form-notification.service';

@Component({
  selector: 'at-assets-form-notification',
  templateUrl: './at-form-notification.component.html',
  styleUrls: ['./at-form-notification.component.scss']
})
export class ATFormNotificationComponent {

  hasNotification : boolean = false;
  exception : ATBusinessExceptionModel;
  errorExceptions : ATBusinessExceptionMessageModel[] = [];
  warningExceptions : ATBusinessExceptionMessageModel[] = [];

  constructor(private cdf: ChangeDetectorRef
    , private atFormNotificationService : ATFormNotificationService) { }

  ngOnInit(){
    this.atFormNotificationService.notification.subscribe((res : ATBusinessExceptionModel) => {
      if(res){
        this.hasNotification = true;
        this.exception = res;
        if(this.exception.Validations?.length > 0){
          this.errorExceptions = this.exception.Validations.filter(x => x.ErrorLevel == ATErrorLevel.Error);
          this.warningExceptions = this.exception.Validations.filter(x => x.ErrorLevel == ATErrorLevel.Warning);
        }
      }else{
        this.hasNotification = false;
        this.exception = null;
        this.errorExceptions = [];
        this.warningExceptions = [];
      }
    });
  }

  ngAfterViewChecked() {
    this.cdf.detectChanges();
  }

}