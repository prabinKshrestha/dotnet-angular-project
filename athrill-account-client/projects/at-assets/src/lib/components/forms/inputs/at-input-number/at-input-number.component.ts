import { Component, forwardRef, Input, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'at-assets-input-number',
  templateUrl: './at-input-number.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputNumberComponent),
      multi: true,
    }
  ],
})

export class ATInputNumberComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() min: number = -999999999999;
  @Input() max: number = 999999999999;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() hint: string;

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