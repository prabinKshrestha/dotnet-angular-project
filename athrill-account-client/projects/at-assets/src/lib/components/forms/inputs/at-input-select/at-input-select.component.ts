import { Component, forwardRef, Input, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'at-assets-input-select',
  templateUrl: './at-input-select.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputSelectComponent),
      multi: true,
    }
  ],
})

export class ATInputSelectComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  @Input() data: any[];
  @Input() textField: string;
  @Input() valueField: string;
  @Input() displayWithFn: Function;
  @Input() valueFn: Function;

  @Output() onChange : EventEmitter<any> = new EventEmitter();
  
  @ViewChild("input") model : NgModel;
  
  onChangeEvent: any = () => {};
  onTouchEvent: any = () => {};
  
  constructor() { }


  ngOnInit(): void {
  }
  
  val = null;
  
  set value(val: string) {
    this.val = val;
    this.onChangeEvent(val);
    this.onTouchEvent(val);
  }

  get value() {
    return this.val;
  }

  writeValue(value: any) {
    this.value = value;
  }
  
  registerOnChange(fn: any) {
    this.onChangeEvent = fn;
  }

  registerOnTouched(fn: any) {
    this.onTouchEvent = fn;
  }

  onSelectionChange(event : any){
    this.onChange.emit(event);
  }

}