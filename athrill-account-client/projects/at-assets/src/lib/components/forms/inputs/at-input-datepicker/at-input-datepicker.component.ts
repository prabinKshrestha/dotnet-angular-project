import { Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewChild } from '@angular/core';
import { DateHelperService } from 'at-services';
import { BehaviorSubject } from 'rxjs';


@Component({
  selector: 'at-assets-input-datepicker',
  templateUrl: './at-input-datepicker.component.html',
  styleUrls: ['./../at-input.component.scss'],
})
export class ATInputDatePickerComponent{

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  @Input() set atDateModel(value: string){
    this.date = value;
  }
  
  valueChanges : BehaviorSubject<any> = new  BehaviorSubject<any>(null);

  get atDateModel(){
    return this.date;
  }

  @Output() atDateModelChange: EventEmitter<string> = new EventEmitter();

  date: any;
  
  constructor(
    private _dateHelperService : DateHelperService
  ) { }

  ngOnInit(): void {
  }

  get isValid(): boolean{
    let retVal = true;
    if(this.required){
        retVal = retVal && this.isDateSelected;
    }
    return retVal;
  }

  get invalid(): boolean {
    return !this.isValid;
  } 


  get isDateSelected(): boolean{
    return this.atDateModel != null;
  }
  
  onDateInput(value: any){
    let date = this._dateHelperService.convertToISOStringWithOutZ(value.value);
    this.atDateModel = date;
    this.atDateModelChange.emit(date);
    this.valueChanges.next(date);
  }

}