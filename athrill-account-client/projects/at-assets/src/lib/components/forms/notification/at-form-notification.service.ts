import { Injectable } from '@angular/core';
import { ATBusinessExceptionModel } from 'at-models';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root',
})

export class ATFormNotificationService {

    public notification : BehaviorSubject<ATBusinessExceptionModel> = new BehaviorSubject<ATBusinessExceptionModel>(null); 

    public clearErrors() : void{
        this.notification.next(null);
    }

    public showErrors(model : ATBusinessExceptionModel) : void{
        this.notification.next(model);
    }

}