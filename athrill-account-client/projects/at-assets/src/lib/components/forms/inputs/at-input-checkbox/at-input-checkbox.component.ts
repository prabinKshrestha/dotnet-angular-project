import { Component, forwardRef, Input, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'at-assets-input-checkbox',
  templateUrl: './at-input-checkbox.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputCheckboxComponent),
      multi: true,
    },
  ],
})
export class ATInputCheckboxComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() labelPosition: 'before' | 'after' = 'after';
  
  @ViewChild("input") model : NgModel;
  
  onChange: any = () => {};
  onTouch: any = () => {};
  
  constructor() { }
  
  
  val = null;
  
  set value(val: string) {
    this.val = val;
    this.onChange(val);
    this.onTouch(val);
  }

  get value() {
    return this.val;
  }

  writeValue(value: any) {
    this.value = value;
  }

  registerOnChange(fn: any) {
    this.onChange = fn;
  }

  registerOnTouched(fn: any) {
    this.onTouch = fn;
  }

  ngOnInit(): void {
  }

}