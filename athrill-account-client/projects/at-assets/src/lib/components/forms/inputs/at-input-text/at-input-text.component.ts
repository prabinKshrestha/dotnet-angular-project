import { Component, forwardRef, Input, OnInit, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'at-assets-input-text',
  templateUrl: './at-input-text.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputTextComponent),
      multi: true,
    }
  ],
})

export class ATInputTextComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() maxLength : number;
  @Input() showLengthCountHint: boolean = true;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() set email (value : boolean){
    this.isEmail = value;
      if(value){
          this.inputType = 'email';
      }
  }
  @Input() inputType : 'password' | 'text' | 'email' = 'text';
  @Input() hint: string;

  @ViewChild("input") model : NgModel;

  isEmail : boolean = false;
  
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