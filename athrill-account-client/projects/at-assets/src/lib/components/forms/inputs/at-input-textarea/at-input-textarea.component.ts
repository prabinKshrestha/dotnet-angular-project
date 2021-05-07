import { Component, forwardRef, Input, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'at-assets-input-textarea',
  templateUrl: './at-input-textarea.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputTextAreaComponent),
      multi: true,
    }
  ],
})

export class ATInputTextAreaComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() maxLength : number;
  @Input() showLengthCountHint: boolean = true;
  @Input() required: boolean = false;
  @Input() hint: string;

  @Input() height: number = 80;
  @Input() maxHeight: number = 240;
  @Input() autoSize: boolean = false;
  
  @ViewChild("input") model : NgModel;
  
  onChangeEvent: any = () => {};
  onTouch: any = () => {};
  
  constructor() { }
  
  val = null;
  
  set value(val: string) {
    this.val = val;
    this.onChangeEvent(val);
    this.onTouch(val);
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
    this.onTouch = fn;
  }

  ngOnInit(): void {
  }

}